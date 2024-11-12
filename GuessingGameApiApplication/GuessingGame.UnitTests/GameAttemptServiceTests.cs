﻿using Moq;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Models;
using GuessingGame.Application.Services;
using GuessingGame.Application.Contracts;
using GuessingGame.Domain.Constants;

namespace GuessingGame.UnitTests
{
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
            _mockSessionRepository.Setup(repo => repo.GetGameDetails(request.SessionId)).ReturnsAsync(game);
            _mockLogicProcessor.Setup(proc => proc.CalculateMatches("5678", "1234")).Returns((12, 22));
            _mockAttemptRepository.Setup(a => a.AddAttempt(
	            It.Is<GameAttemptModel>(x => 
		            x.AttemptNumber == 1 &&
		            x.PositionMatch == 12 &&
		            x.MatchInIncorrectPositions == 22 &&
		            x.GuessedNumber == "1234"))).Returns(Task.FromResult(1));
            _mockLogicProcessor.Setup(proc => proc.GameFinished(22, 1, request.SessionId)).ReturnsAsync(false);

            // Act
            var result = await _gameAttemptService.ProcessAttemptAsync(request);

            // Assert
            _mockSessionRepository.Verify(repo => repo.GetGameDetails(request.SessionId), Times.Once);
            _mockLogicProcessor.Verify(proc => proc.CalculateMatches("5678", "1234"), Times.Once);
            _mockAttemptRepository.Verify(a => a.AddAttempt(
	            It.Is<GameAttemptModel>(x => 
		            x.AttemptNumber == 1 &&
		            x.PositionMatch == 12 &&
		            x.MatchInIncorrectPositions == 22 &&
		            x.GuessedNumber == "1234")), Times.Once);
            _mockLogicProcessor.Verify(proc => proc.GameFinished(22, 1, request.SessionId), Times.Once);

            Assert.NotNull(result);
            Assert.False(result.IsCompleted);
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
	        _mockSessionRepository.Setup(repo => repo.GetGameDetails(request.SessionId)).ReturnsAsync(game);
	        _mockLogicProcessor.Setup(proc => proc.CalculateMatches("5678", "1234")).Returns((12, 22));
	        _mockAttemptRepository.Setup(a => a.AddAttempt(
		        It.Is<GameAttemptModel>(x => 
			        x.AttemptNumber == 1 &&
			        x.PositionMatch == 12 &&
			        x.MatchInIncorrectPositions == 22 &&
			        x.GuessedNumber == "1234"))).Returns(Task.FromResult(1));
	        _mockLogicProcessor.Setup(proc => proc.GameFinished(22, 1, request.SessionId)).ReturnsAsync(true);

	        // Act
	        var result = await _gameAttemptService.ProcessAttemptAsync(request);

	        // Assert
	        _mockSessionRepository.Verify(repo => repo.GetGameDetails(request.SessionId), Times.Once);
	        _mockLogicProcessor.Verify(proc => proc.CalculateMatches("5678", "1234"), Times.Once);
	        _mockAttemptRepository.Verify(a => a.AddAttempt(
		        It.Is<GameAttemptModel>(x => 
			        x.AttemptNumber == 1 &&
			        x.PositionMatch == 12 &&
			        x.MatchInIncorrectPositions == 22 &&
			        x.GuessedNumber == "1234")), Times.Once);
	        _mockLogicProcessor.Verify(proc => proc.GameFinished(22, 1, request.SessionId), Times.Once);

	        Assert.NotNull(result);
	        Assert.True(result.IsCompleted);
	        Assert.That(result.PositionMatch, Is.EqualTo(0));
        }

        [Test]
        public async Task ProcessAttemptAsync_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
            var request = new AttemptRequest { SessionId = guid };
            _mockSessionRepository.Setup(repo => repo.GetGameDetails(guid)).ThrowsAsync(new Exception());

            // Act
            var result = await _gameAttemptService.ProcessAttemptAsync(request);

            // Assert
            _mockSessionRepository.Verify(repo => repo.GetGameDetails(guid), Times.Once);
            Assert.IsNull(result);
        }
    }
}