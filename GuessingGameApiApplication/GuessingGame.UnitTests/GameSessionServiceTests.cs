using GuessingGame.Application.Services;
using Moq;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;

namespace GuessingGame.UnitTests;

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
		_mockSessionRepository.Setup(repo => repo.AddGameSession("Player1", "1234")).ReturnsAsync(guid);

		// Act
		var result = await _gameSessionService.StartNewGame("Player1");

		// Assert
		_mockSessionRepository.Verify(repo => repo.AddGameSession("Player1", "1234"), Times.Once);
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.EqualTo(guid));
	}

	[Test]
	public async Task StartNewGame_ShouldReturnNull_WhenExceptionIsThrown()
	{
		// Arrange

		// Act
		var result = await _gameSessionService.StartNewGame("Player1");

		// Assert
		Assert.That(result, Is.Null);
	}
	
	/*[Test]
	public void GenerateUniqueFourDigitNumber_ShouldReturnUniqueFourDigitNumber()
	{
		// Act
		var result = _gameLogicProcessor.GenerateUniqueFourDigitNumber();

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Length.EqualTo(4));
		Assert.That(int.TryParse(result, out _), Is.True);
		Assert.That(new HashSet<char>(result), Has.Count.EqualTo(4));
	}*/
}