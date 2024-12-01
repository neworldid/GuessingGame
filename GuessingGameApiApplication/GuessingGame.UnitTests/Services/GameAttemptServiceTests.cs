using GuessingGame.Application.Services;
using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;
using Moq;

namespace GuessingGame.UnitTests.Services;

public class GameAttemptServiceTests
{
	private Mock<IGameSessionRepository> _mockSessionRepository;
	private Mock<IGameAttemptRepository> _mockAttemptRepository;
	private Mock<IGameLogicProcessor> _mockLogicProcessor;
	private GameAttemptService _gameAttemptService;

	[SetUp]
	public void Setup()
	{
		_mockSessionRepository = new Mock<IGameSessionRepository>();
		_mockAttemptRepository = new Mock<IGameAttemptRepository>();
		_mockLogicProcessor = new Mock<IGameLogicProcessor>();
		_gameAttemptService = new GameAttemptService(
			_mockSessionRepository.Object,
			_mockAttemptRepository.Object,
			_mockLogicProcessor.Object);
	}

	[Test]
	public async Task ProcessAttemptAsync_ShouldReturnAttemptResponse_WhenGameIsNotFinished()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		var request = new AttemptRequest { SessionId = guid, Number = "1234" };
		var game = new GameDetailsModel { SecretNumber = "5678", AttemptCount = 0 };
		// remove game and add setup for getting secret number
		_mockSessionRepository.Setup(repo => repo.GetSecretNumber(request.SessionId)).ReturnsAsync("5678");
		_mockLogicProcessor.Setup(proc => proc.CalculateMatches("5678", "1234")).Returns((12, 22));
		_mockAttemptRepository.Setup(a => a.AddAttempt(
			It.Is<GameAttemptModel>(x => 
				x.AttemptNumber == 1 &&
				x.PositionMatch == 12 &&
				x.MatchInIncorrectPositions == 22 &&
				x.GuessedNumber == "1234"))).Returns(Task.FromResult(1));
		_mockLogicProcessor.Setup(proc => proc.GameFinished(12, 1, request.SessionId)).ReturnsAsync(false);

		// Act
		var result = await _gameAttemptService.ProcessAttemptAsync(request);

		// Assert
		_mockSessionRepository.Verify(repo => repo.GetSecretNumber(request.SessionId), Times.Once);
		_mockLogicProcessor.Verify(proc => proc.CalculateMatches("5678", "1234"), Times.Once);
		_mockAttemptRepository.Verify(a => a.AddAttempt(
			It.Is<GameAttemptModel>(x => 
				x.AttemptNumber == 1 &&
				x.PositionMatch == 12 &&
				x.MatchInIncorrectPositions == 22 &&
				x.GuessedNumber == "1234")), Times.Once);
		_mockLogicProcessor.Verify(proc => proc.GameFinished(12, 1, request.SessionId), Times.Once);

		Assert.That(result, Is.Not.Null);
		Assert.That(result.IsCompleted, Is.False);
		Assert.That(result.PositionMatch, Is.EqualTo(12));
		Assert.That(result.MatchInIncorrectPositions, Is.EqualTo(22));
		Assert.That(result.TriesLeft, Is.EqualTo(GameConstants.MaxAttempts - 1));
	}

	[Test]
	public async Task ProcessAttemptAsync_ShouldReturnCompletedAttemptResponse_WhenGameIsFinished()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		var request = new AttemptRequest { SessionId = guid, Number = "1234" };
		var game = new GameDetailsModel { SecretNumber = "5678", AttemptCount = 0 };
		_mockSessionRepository.Setup(repo => repo.GetSecretNumber(request.SessionId)).ReturnsAsync("5678");
		_mockLogicProcessor.Setup(proc => proc.CalculateMatches("5678", "1234")).Returns((12, 22));
		_mockAttemptRepository.Setup(a => a.AddAttempt(
			It.Is<GameAttemptModel>(x => 
				x.AttemptNumber == 1 &&
				x.PositionMatch == 12 &&
				x.MatchInIncorrectPositions == 22 &&
				x.GuessedNumber == "1234"))).Returns(Task.FromResult(1));
		_mockLogicProcessor.Setup(proc => proc.GameFinished(12, 1, request.SessionId)).ReturnsAsync(true);

		// Act
		var result = await _gameAttemptService.ProcessAttemptAsync(request);

		// Assert
		_mockSessionRepository.Verify(repo => repo.GetSecretNumber(request.SessionId), Times.Once);
		_mockLogicProcessor.Verify(proc => proc.CalculateMatches("5678", "1234"), Times.Once);
		_mockAttemptRepository.Verify(a => a.AddAttempt(
			It.Is<GameAttemptModel>(x => 
				x.AttemptNumber == 1 &&
				x.PositionMatch == 12 &&
				x.MatchInIncorrectPositions == 22 &&
				x.GuessedNumber == "1234")), Times.Once);
		_mockLogicProcessor.Verify(proc => proc.GameFinished(12, 1, request.SessionId), Times.Once);

		Assert.That(result, Is.Not.Null);
		Assert.That(result.IsCompleted, Is.True);
		Assert.That(result.PositionMatch, Is.EqualTo(0));
	}

	[Test]
	public async Task ProcessAttemptAsync_ShouldReturnNull_WhenExceptionIsThrown()
	{
		// Arrange
		var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
		var request = new AttemptRequest { SessionId = guid };
		_mockSessionRepository.Setup(repo => repo.GetSecretNumber(guid)).ThrowsAsync(new Exception());

		// Act
		var result = await _gameAttemptService.ProcessAttemptAsync(request);

		// Assert
		_mockSessionRepository.Verify(repo => repo.GetSecretNumber(guid), Times.Once);
		Assert.That(result, Is.Null);
	}
}