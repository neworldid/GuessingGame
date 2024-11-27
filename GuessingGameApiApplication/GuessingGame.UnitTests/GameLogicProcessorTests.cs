using GuessingGame.Application.Processors;
using Moq;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;

namespace GuessingGame.UnitTests;

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
	public async Task GameFinished_ShouldReturnTrue_WhenAllMatchesAreCorrect()
	{
		// Arrange
		var sessionId = Guid.NewGuid();
		_mockResultRepository.Setup(repo => repo.AddGameResultAndEndSessionAsync(sessionId, 1, true));

		// Act
		var result = await _gameLogicProcessor.GameFinished(GameConstants.SecretNumberLength, 1, sessionId);

		// Assert
		Assert.That(result, Is.True);
		_mockResultRepository.Verify(repo => repo.AddGameResultAndEndSessionAsync(sessionId, 1, true), Times.Once);
	}

	[Test]
	public async Task GameFinished_ShouldReturnTrue_WhenMaxAttemptsReached()
	{
		// Arrange
		var sessionId = Guid.NewGuid();
		_mockResultRepository.Setup(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, false));

		// Act
		var result = await _gameLogicProcessor.GameFinished(0, GameConstants.MaxAttempts, sessionId);

		// Assert
		Assert.That(result, Is.True);
		_mockResultRepository.Verify(repo => repo.AddGameResultAndEndSessionAsync(sessionId, GameConstants.MaxAttempts, false), Times.Once);
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