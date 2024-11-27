using GuessingGame.API.Controllers;
using GuessingGame.Domain.Abstractions.Repositories;
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
		Assert.That(result, Is.InstanceOf<OkResult>());        }

	[Test]
	public async Task DeleteGameSessionData_ReturnsNotFound_WhenExceptionThrown()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		_mockGameSessionRepository.Setup(repo => repo.DeleteSessionsAsync(guid))
			.ThrowsAsync(new Exception());

		// Act
		var result = await _controller.DeleteGameSessionData(guid);

		// Assert
		_mockGameSessionRepository.Verify(repo => repo.DeleteSessionsAsync(guid), Times.Once);
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
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
		Assert.That(result, Is.InstanceOf<NotFoundResult>());
	}
}