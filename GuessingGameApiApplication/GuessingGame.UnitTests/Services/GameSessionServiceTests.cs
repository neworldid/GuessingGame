using GuessingGame.Application.Services;
using Moq;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;

namespace GuessingGame.UnitTests.Services;

public class GameSessionServiceTests
{
	private Mock<IGameSessionRepository> _mockSessionRepository;
	private GameSessionService _gameSessionService;

	[SetUp]
	public void Setup()
	{
		_mockSessionRepository = new Mock<IGameSessionRepository>();
		_gameSessionService = new GameSessionService(_mockSessionRepository.Object);
	}

	[Test]
	public async Task StartNewGame_ShouldReturnGuid_WhenGameIsCreated()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		_mockSessionRepository.Setup(repo => repo.AddGameSession("Player1", It.Is<string>(x => 
			x.Length == GameConstants.SecretNumberLength && x.All(char.IsDigit) == true))).ReturnsAsync(guid);

		// Act
		var result = await _gameSessionService.StartNewGame("Player1");

		// Assert
		_mockSessionRepository.Verify(repo => repo.AddGameSession("Player1", It.Is<string>(x => 
			x.Length == GameConstants.SecretNumberLength && x.All(char.IsDigit) == true)), Times.Once);
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.EqualTo(guid));
	}

	[Test]
	public async Task StartNewGame_ShouldReturnNull_WhenExceptionIsThrown()
	{
		// Arrange
		_mockSessionRepository.Setup(repo => repo.AddGameSession("Player1", It.Is<string>(x => 
			x.Length == GameConstants.SecretNumberLength && x.All(char.IsDigit) == true))).ThrowsAsync(new Exception());
		
		// Act
		var result = await _gameSessionService.StartNewGame("Player1");

		// Assert
		Assert.That(result, Is.Null);
	}
}