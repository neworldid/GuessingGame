using GuessingGame.API.Controllers;
using GuessingGame.API.Contracts;
using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GuessingGame.UnitTests.Controllers;

public class GameSettingsControllerTests
{
	private Mock<IGameSettingRepository> _mockGameSettingRepository;
	private GameSettingsController _controller;

	[SetUp]
	public void Setup()
	{
		_mockGameSettingRepository = new Mock<IGameSettingRepository>();
		_controller = new GameSettingsController(_mockGameSettingRepository.Object);
	}

	[Test]
	public async Task GetGameSetting_ReturnsOkResult_WithSettingValue()
	{
		// Arrange
		const int gameSettingId = 1;
		const bool settingValue = true;
		_mockGameSettingRepository.Setup(repo => repo.GetSettingValueAsync(gameSettingId))
			.ReturnsAsync(settingValue);

		// Act
		var result = await _controller.GetGameSetting(gameSettingId);

		// Assert
		_mockGameSettingRepository.Verify(repo => repo.GetSettingValueAsync(gameSettingId), Times.Once);
		var okResult = result as OkObjectResult;
		Assert.That(okResult, Is.Not.Null);
		Assert.That(okResult.Value, Is.EqualTo(settingValue));
	}

	[Test]
	public async Task GetGameSetting_ReturnsNotFound_OnException()
	{
		// Arrange
		_mockGameSettingRepository.Setup(repo => repo.GetSettingValueAsync(1))
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.GetGameSetting(1);

		// Assert
		_mockGameSettingRepository.Verify(repo => repo.GetSettingValueAsync(1), Times.Once);
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
	}

	[Test]
	public async Task UpdateGameSetting_ReturnsOkResult_WhenUpdateIsSuccessful()
	{
		// Arrange
		var request = new GameSettingRequest { Id = 1, IsEnabled = true };
		_mockGameSettingRepository.Setup(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled))
			.ReturnsAsync(1);

		// Act
		var result = await _controller.UpdateGameSetting(request);

		// Assert
		_mockGameSettingRepository.Verify(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled),
			Times.Once);
		Assert.That(result, Is.InstanceOf<OkResult>());
	}

	[Test]
	public async Task UpdateGameSetting_ReturnsNotFound_WhenUpdateFails()
	{
		// Arrange
		var request = new GameSettingRequest { Id = 1, IsEnabled = true };
		_mockGameSettingRepository.Setup(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled))
			.ReturnsAsync(0);

		// Act
		var result = await _controller.UpdateGameSetting(request);

		// Assert
		_mockGameSettingRepository.Verify(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled),
			Times.Once);
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
	}

	[Test]
	public async Task UpdateGameSetting_ReturnsBadRequest_OnException()
	{
		// Arrange
		var request = new GameSettingRequest { Id = 1, IsEnabled = true };
		_mockGameSettingRepository.Setup(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled))
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.UpdateGameSetting(request);

		// Assert
		_mockGameSettingRepository.Verify(repo => repo.UpdateSettingValueAsync(request.Id, request.IsEnabled),
			Times.Once);
		Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
	}
}