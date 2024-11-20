using GuessingGame.Application.Services;
using Moq;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;

namespace GuessingGame.UnitTests
{
    public class GameSessionServiceTests
    {
        private Mock<IGameSessionRepository> _mockSessionRepository;
        private Mock<IGameLogicProcessor> _mockLogicProcessor;
        private GameSessionService _gameSessionService;

        [SetUp]
        public void Setup()
        {
            _mockSessionRepository = new Mock<IGameSessionRepository>();
            _mockLogicProcessor = new Mock<IGameLogicProcessor>();
            _gameSessionService = new GameSessionService(
                _mockSessionRepository.Object,
                _mockLogicProcessor.Object);
        }

        [Test]
        public async Task StartNewGame_ShouldReturnGuid_WhenGameIsCreated()
        {
            // Arrange
            var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
            _mockLogicProcessor.Setup(proc => proc.GenerateUniqueFourDigitNumber()).Returns("1234");
            _mockSessionRepository.Setup(repo => repo.AddGameSession("Player1", "1234")).ReturnsAsync(guid);

            // Act
            var result = await _gameSessionService.StartNewGame("Player1");

            // Assert
            _mockLogicProcessor.Verify(proc => proc.GenerateUniqueFourDigitNumber(), Times.Once);
            _mockSessionRepository.Verify(repo => repo.AddGameSession("Player1", "1234"), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(guid));
        }

        [Test]
        public async Task StartNewGame_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            _mockLogicProcessor.Setup(proc => proc.GenerateUniqueFourDigitNumber()).Throws(new Exception());

            // Act
            var result = await _gameSessionService.StartNewGame("Player1");

            // Assert
            _mockLogicProcessor.Verify(proc => proc.GenerateUniqueFourDigitNumber(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetGameDetails_ShouldReturnGameResultResponse_WhenGameExists()
        {
            // Arrange
            var guid = new Guid("d2719b1e-1c4b-4b8e-8b1e-1c4b4b8e8b1e");
            var game = new GameDetailsModel
            {
                SecretNumber = "1234",
                AttemptCount = 5,
                StartTime = DateTime.Now,
                Won = true
            };
            game.EndTime = game.StartTime.AddMinutes(5);
            
            _mockSessionRepository.Setup(repo => repo.GetGameDetails(guid)).ReturnsAsync(game);

            // Act
            var result = await _gameSessionService.GetGameDetails(guid);

            // Assert
            _mockSessionRepository.Verify(repo => repo.GetGameDetails(guid), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SecretNumber, Is.EqualTo("1234"));
            Assert.That(result.AttemptCount, Is.EqualTo(5));
            Assert.That(result.Duration, Is.EqualTo("05:00"));
            Assert.That(result.Won, Is.EqualTo(true));
        }

        [Test]
        public async Task GetGameDetails_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            _mockSessionRepository.Setup(repo => repo.GetGameDetails(sessionId)).Throws(new Exception());

            // Act
            var result = await _gameSessionService.GetGameDetails(sessionId);

            // Assert
            _mockSessionRepository.Verify(repo => repo.GetGameDetails(sessionId), Times.Once);
            Assert.That(result, Is.Null);
        }
    }
}