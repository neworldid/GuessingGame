using GuessingGame.API.Controllers;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GuessingGame.UnitTests.Controllers;

public class GameAdminControllerTests
{
	private Mock<IGameAttemptRepository> _mockGameAttemptRepository;
	private Mock<IGameSessionRepository> _mockGameSessionRepository;
	private GameAdminController _controller;
	
	[SetUp]
	public void Setup()
	{
		_mockGameAttemptRepository = new Mock<IGameAttemptRepository>();
		_mockGameSessionRepository = new Mock<IGameSessionRepository>();
		_controller = new GameAdminController(_mockGameAttemptRepository.Object, _mockGameSessionRepository.Object);
	}
	
	[Test]
	public async Task GetAllSessions_ReturnsOkResult_WithListOfSessions()
	{
		// Arrange
		var gameSessionDetailList = new List<GameDetailsModel>
		{
			new()
		};
		_mockGameSessionRepository.Setup(repo => repo.GetAllGameSessions())
			.ReturnsAsync(gameSessionDetailList);

		// Act
		var result = await _controller.GetAllSessions();

		// Assert
		_mockGameSessionRepository.Verify(repo => repo.GetAllGameSessions(), Times.Once);
		var okResult = result as OkObjectResult;
		Assert.That(okResult, Is.Not.Null);
		var sessions = okResult.Value as List<GameDetailsModel>;
		Assert.That(sessions, Is.Not.Null);
		Assert.That(sessions.Count, Is.EqualTo(1));
	}

	[Test]
	public async Task GetAllSessions_ReturnsBadRequest_OnException()
	{
		// Arrange
		_mockGameSessionRepository.Setup(repo => repo.GetAllGameSessions())
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.GetAllSessions();

		// Assert
		_mockGameSessionRepository.Verify(repo => repo.GetAllGameSessions(), Times.Once);
		Assert.That(result, Is.InstanceOf<BadRequestResult>());
	}
	
	[Test]
	public async Task GetGameSessionAttempts_ReturnsOkResult_WithListOfAttempts()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		var attempts = new List<GameAttemptModel> { new(guid, "1232", 1, 2, 1) };
		_mockGameAttemptRepository.Setup(repo => repo.GetAttempts(guid))
			.ReturnsAsync(attempts);

		// Act
		var result = await _controller.GetGameSessionAttempts(guid);

		// Assert
		_mockGameAttemptRepository.Verify(repo => repo.GetAttempts(guid), Times.Once);
		var okResult = result as OkObjectResult;
		Assert.That(okResult, Is.Not.Null);
		var returnedAttempts = okResult.Value as List<GameAttemptModel>;
		Assert.That(returnedAttempts, Is.Not.Null);
		Assert.That(returnedAttempts.Count, Is.EqualTo(1));
	}

	[Test]
	public async Task GetGameSessionAttempts_ReturnsBadRequest_OnException()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		_mockGameAttemptRepository.Setup(repo => repo.GetAttempts(guid))
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.GetGameSessionAttempts(guid);

		// Assert
		_mockGameAttemptRepository.Verify(repo => repo.GetAttempts(guid), Times.Once);
		Assert.That(result, Is.InstanceOf<BadRequestResult>());
	}
	
	[Test]
	public async Task DeleteGameSessionData_ReturnsOkResult()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		_mockGameSessionRepository.Setup(repo => repo.DeleteSessionsAsync(guid))
			.ReturnsAsync(true);

		// Act
		var result = await _controller.DeleteGameSessionData(guid);

		// Assert
		_mockGameSessionRepository.Verify(repo => repo.DeleteSessionsAsync(guid), Times.Once);
		Assert.That(result, Is.InstanceOf<OkResult>());        
	}
        
	[Test]
	public async Task DeleteGameSessionData_ReturnsNotFound_WhenResultWasNotDeleted()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		_mockGameSessionRepository.Setup(repo => repo.DeleteSessionsAsync(guid))
			.ReturnsAsync(false);

		// Act
		var result = await _controller.DeleteGameSessionData(guid);

		// Assert
		_mockGameSessionRepository.Verify(repo => repo.DeleteSessionsAsync(guid), Times.Once);
		Assert.That(result, Is.InstanceOf<BadRequestResult>());
	}
}