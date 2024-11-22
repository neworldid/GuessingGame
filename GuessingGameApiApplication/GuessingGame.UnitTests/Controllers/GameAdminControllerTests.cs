using GuessingGame.API.Controllers;
using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GuessingGame.UnitTests.Controllers;

public class GameAdminControllerTests
{
	private Mock<IGameResultRepository> _mockGameResultRepository;
	private GameAdminController _controller;
	
	[SetUp]
	public void Setup()
	{
		_mockGameResultRepository = new Mock<IGameResultRepository>();
		_controller = new GameAdminController(_mockGameResultRepository.Object);
	}
	
	[Test]
	public async Task DeleteResult_ReturnsOkResult()
	{
		// Arrange
		var resultId = 1;
		_mockGameResultRepository.Setup(repo => repo.DeleteGameResult(resultId))
			.ReturnsAsync(1);

		// Act
		var result = await _controller.DeleteResult(resultId);

		// Assert
		_mockGameResultRepository.Verify(repo => repo.DeleteGameResult(resultId), Times.Once);
		Assert.That(result, Is.InstanceOf<OkResult>());        }

	[Test]
	public async Task DeleteResult_ReturnsNotFound_WhenExceptionThrown()
	{
		// Arrange
		var resultId = 1;
		_mockGameResultRepository.Setup(repo => repo.DeleteGameResult(resultId))
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.DeleteResult(resultId);

		// Assert
		_mockGameResultRepository.Verify(repo => repo.DeleteGameResult(resultId), Times.Once);
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
	}
        
	[Test]
	public async Task DeleteResult_ReturnsNotFound_WhenResultWasNotDeleted()
	{
		// Arrange
		_mockGameResultRepository.Setup(repo => repo.DeleteGameResult(1))
			.ReturnsAsync(0);

		// Act
		var result = await _controller.DeleteResult(1);

		// Assert
		_mockGameResultRepository.Verify(repo => repo.DeleteGameResult(1), Times.Once);
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
	}
}