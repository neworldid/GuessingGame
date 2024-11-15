using GuessingGame.API.Contracts;
using GuessingGame.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;
using GuessingGame.Domain.Models;

namespace GuessingGame.UnitTests
{
    public class GameControllerTests
    {
        private Mock<IGameSessionService> _mockGameSessionService;
        private Mock<IGameAttemptService> _mockGameAttemptService;
        private Mock<IGameResultService> _mockGameResultService;
        private Mock<IGameResultRepository> _mockGameResultRepository;
        private GameController _controller;

        [SetUp]
        public void Setup()
        {
            _mockGameSessionService = new Mock<IGameSessionService>();
            _mockGameAttemptService = new Mock<IGameAttemptService>();
            _mockGameResultService = new Mock<IGameResultService>();
            _mockGameResultRepository = new Mock<IGameResultRepository>();
            _controller = new GameController(
                _mockGameSessionService.Object,
                _mockGameAttemptService.Object,
                _mockGameResultService.Object,
                _mockGameResultRepository.Object
            );
        }

        [Test]
        public async Task StartGame_ReturnsBadRequest()
        {
	        // Arrange
	        var playerRequest = new PlayerRequest { PlayerName = "test-player" };
	        _mockGameSessionService.Setup(service => service.StartNewGame("test-player"))
		        .ReturnsAsync((Guid?)null);

	        // Act
	        var result = await _controller.StartGame(playerRequest);

	        // Assert
	        _mockGameSessionService.Verify(s => s.StartNewGame("test-player"), Times.Once);
	        Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task StartGame_ReturnsOkResult()
        {
            // Arrange
            var playerRequest = new PlayerRequest { PlayerName = "test-player" };
            _mockGameSessionService.Setup(service => service.StartNewGame("test-player"))
                .ReturnsAsync(new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e"));

            // Act
            var result = await _controller.StartGame(playerRequest);

            // Assert
            _mockGameSessionService.Verify(s => s.StartNewGame("test-player"), Times.Once);
            Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ProcessAttempt_ReturnsBadRequest()
        {
	        // Arrange
	        var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
	        var playerRequest = new AttemptRequest { Number = "123", SessionId = guid };
	        
	        _mockGameAttemptService.Setup(service => service.ProcessAttemptAsync(playerRequest))
		        .ReturnsAsync((AttemptResponse?)null);

	        // Act
	        var result = await _controller.ProcessAttempt(playerRequest);

	        // Assert
	        _mockGameAttemptService.Verify(s => s.ProcessAttemptAsync(It.Is<AttemptRequest>(x => 
		        x.Number == "123" &&
		        x.SessionId == guid)), Times.Once);
	        Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task ProcessAttempt_ReturnsOkResult()
        {
            // Arrange
            var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
            var playerRequest = new AttemptRequest { Number = "123", SessionId = guid };
            var attemptResponse = new AttemptResponse { PositionMatch = 1, MatchInIncorrectPositions = 1, TriesLeft = 5 };
	        
            _mockGameAttemptService.Setup(service => service.ProcessAttemptAsync(playerRequest))
	            .ReturnsAsync(attemptResponse);

            // Act
            var result = await _controller.ProcessAttempt(playerRequest);

            // Assert
            _mockGameAttemptService.Verify(s => s.ProcessAttemptAsync(It.Is<AttemptRequest>(x => 
	            x.Number == "123" &&
	            x.SessionId == guid)), Times.Once);
            Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(200));
            
            Assert.That((result as ObjectResult)?.Value, Is.TypeOf<AttemptResponse>());
            
            var attemptResponseFromResut = (result as ObjectResult)?.Value as AttemptResponse;
            Assert.That(attemptResponseFromResut?.PositionMatch, Is.EqualTo(1));
            Assert.That(attemptResponseFromResut?.MatchInIncorrectPositions, Is.EqualTo(1));
            Assert.That(attemptResponseFromResut?.TriesLeft, Is.EqualTo(5));
        }

        [Test]
        public async Task GetGameDetails_ReturnsNoContent()
        {
	        // Arrange
	        var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
	        _mockGameSessionService.Setup(service => service.GetGameDetails(guid))
		        .ReturnsAsync((GameResultResponse?) null);

	        // Act
	        var result = await _controller.GetGameDetails(guid);

	        // Assert
	        _mockGameSessionService.Verify(s => s.GetGameDetails(guid), Times.Once);
	        Assert.That((result as NotFoundResult)?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetGameDetails_ReturnsOkResult()
        {
	        // Arrange
	        var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
	        var resultResponse = new GameResultResponse { PlayerName = "test-player", SecretNumber = "1234", AttemptCount = 1, Duration = "00:00", Won = true };
	        _mockGameSessionService.Setup(service => service.GetGameDetails(guid))
		        .ReturnsAsync(resultResponse);

	        // Act
	        var result = await _controller.GetGameDetails(guid);

	        // Assert
	        _mockGameSessionService.Verify(s => s.GetGameDetails(guid), Times.Once);
	        Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(200));
            
	        Assert.That((result as ObjectResult)?.Value, Is.TypeOf<GameResultResponse>());
            
	        var resultResponseFromResult = (result as ObjectResult)?.Value as GameResultResponse;
	        Assert.That(resultResponseFromResult?.PlayerName, Is.EqualTo("test-player"));
	        Assert.That(resultResponseFromResult?.SecretNumber, Is.EqualTo("1234"));
	        Assert.That(resultResponseFromResult?.AttemptCount, Is.EqualTo(1));
	        Assert.That(resultResponseFromResult?.Duration, Is.EqualTo("00:00"));
	        Assert.That(resultResponseFromResult?.Won, Is.EqualTo(true));
        }

        [Test]
        public async Task GetGameResults_ReturnsNoContent()
        {
	        // Arrange
	        _mockGameResultService.Setup(service => service.GetGameResults())
		        .ReturnsAsync((IEnumerable<GameResultResponse>?)null);

	        // Act
	        var result = await _controller.GetGameResults();

	        // Assert
	        _mockGameResultService.Verify(s => s.GetGameResults(), Times.Once);
	        Assert.That((result as NoContentResult)?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task GetGameResults_ReturnsOkResult()
        {
	        // Arrange
	        var list = new List<GameResultResponse> { new() { PlayerName = "player-name", SecretNumber = "1235"} };
	        _mockGameResultService.Setup(service => service.GetGameResults())
		        .ReturnsAsync(list);

	        // Act
	        var result = await _controller.GetGameResults();

	        // Assert
	        Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(200));
            
	        Assert.That((result as ObjectResult)?.Value, Is.TypeOf<List<GameResultResponse>>());
            
	        var gameResultResponses = (result as ObjectResult)?.Value as List<GameResultResponse>;
	        Assert.That(gameResultResponses.FirstOrDefault().PlayerName, Is.EqualTo("player-name"));
	        Assert.That(gameResultResponses.FirstOrDefault()?.SecretNumber, Is.EqualTo("1235"));
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
}