using Moq;
using GuessingGame.Application.Services;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;

namespace GuessingGame.UnitTests
{
    public class GameResultServiceTests
    {
        private Mock<IGameResultRepository> _mockGameResultRepository;
        private GameResultService _gameResultService;

        [SetUp]
        public void Setup()
        {
            _mockGameResultRepository = new Mock<IGameResultRepository>();
            _gameResultService = new GameResultService(_mockGameResultRepository.Object);
        }

        [Test]
        public async Task GetGameResults_ShouldReturnGameResultResponses_WhenResultsExist()
        {
            // Arrange
            var gameResults = new List<GameDetailsModel>
            {
                new()
                {
                    GameResultId = 1,
                    PlayerName = "Player1",
                    SecretNumber = "1234",
                    AttemptCount = 5,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(5),
                    Won = false
                }
            };
            _mockGameResultRepository.Setup(repo => repo.GetGameResults()).ReturnsAsync(gameResults);

            // Act
            var result = await _gameResultService.GetGameResults();

            // Assert
            Assert.That(result, Is.Not.Null);
            var firstResult = result.First();
            Assert.That(firstResult.Id, Is.EqualTo(1));
            Assert.That(firstResult.PlayerName, Is.EqualTo("Player1"));
            Assert.That(firstResult.SecretNumber, Is.EqualTo("1234"));
            Assert.That(firstResult.AttemptCount, Is.EqualTo(5));
            Assert.That(firstResult.Duration, Is.EqualTo("05:00"));
            Assert.That(firstResult.Won, Is.EqualTo(false));
        }

        [Test]
        public async Task GetGameResults_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            _mockGameResultRepository.Setup(repo => repo.GetGameResults()).ThrowsAsync(new Exception());

            // Act
            var result = await _gameResultService.GetGameResults();

            // Assert
            _mockGameResultRepository.Verify(repo => repo.GetGameResults(), Times.Once);
            Assert.That(result, Is.Null);
        }
    }
}