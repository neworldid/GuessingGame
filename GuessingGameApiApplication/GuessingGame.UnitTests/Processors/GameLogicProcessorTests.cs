using GuessingGame.Application.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;
using Moq;

namespace GuessingGame.UnitTests.Processors;

public class GameLogicProcessorTests
{
	private Mock<IGameResultRepository> _mockResultRepository;
	private GameLogicProcessor _gameLogicProcessor;

	[SetUp]
	public void Setup()
	{
		_mockResultRepository = new Mock<IGameResultRepository>();
		_gameLogicProcessor = new GameLogicProcessor(_mockResultRepository.Object);
	}

	[TestCase("1234", "1234", 4, 0)]
	[TestCase("1234", "4321", 0, 4)]
	[TestCase("1234", "5678", 0, 0)]
	[TestCase("1234", "1243", 2, 2)]
	public void CalculateMatches_ShouldReturnCorrectMatches(string secretNumber, string guessedNumber, int expectedPositionMatch, int expectedMatchInIncorrectPositions)
	{
		// Act
		var (positionMatch, matchInIncorrectPositions) = _gameLogicProcessor.CalculateMatches(secretNumber, guessedNumber);

		// Assert
		Assert.That(positionMatch, Is.EqualTo(expectedPositionMatch));
		Assert.That(matchInIncorrectPositions, Is.EqualTo(expectedMatchInIncorrectPositions));
	}

	[Test]
	public async Task GameFinished_ShouldReturnTrue_WhenMaxAttemptsReached()
	{
		// Arrange
		var sessionId = Guid.NewGuid();
		_mockResultRepository.Setup(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, false)).ReturnsAsync(true);

		// Act
		var result = await _gameLogicProcessor.GameFinished(2, GameConstants.MaxAttempts, sessionId);

		// Assert
		Assert.That(result, Is.True);
		_mockResultRepository.Verify(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, false), Times.Once);
	}

	[Test]
	public async Task GameFinished_ShouldReturnFalse_WhenGameFinishQueryReturnedFalse()
	{
		// Arrange
		var sessionId = Guid.NewGuid();
		_mockResultRepository.Setup(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, true)).ReturnsAsync(false);

		// Act
		var result = await _gameLogicProcessor.GameFinished(GameConstants.SecretNumberLength, GameConstants.MaxAttempts, sessionId);

		// Assert
		Assert.That(result, Is.False);
		_mockResultRepository.Verify(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, true), Times.Once);
	}

	[Test]
	public async Task GameFinished_ShouldReturnFalse_WhenGameIsNotFinished()
	{
		// Arrange
		var sessionId = Guid.NewGuid();

		// Act
		var result = await _gameLogicProcessor.GameFinished(1, 1, sessionId);

		// Assert
		Assert.That(result, Is.False);
		_mockResultRepository.Verify(repo => repo.AddGameResultAndEndSessionAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Never);
	}
}