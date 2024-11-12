using Moq;
using GuessingGame.Application.Processors;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Constants;

namespace GuessingGame.UnitTests
{
    public class GameLogicProcessorTests
    {
        private Mock<IGameSessionRepository> _mockSessionRepository;
        private Mock<IGameResultRepository> _mockResultRepository;
        private GameLogicProcessor _gameLogicProcessor;

        [SetUp]
        public void Setup()
        {
            _mockSessionRepository = new Mock<IGameSessionRepository>();
            _mockResultRepository = new Mock<IGameResultRepository>();
            _gameLogicProcessor = new GameLogicProcessor(
                _mockSessionRepository.Object,
                _mockResultRepository.Object);
        }

        [Test]
        public void GenerateUniqueFourDigitNumber_ShouldReturnUniqueFourDigitNumber()
        {
            // Act
            var result = _gameLogicProcessor.GenerateUniqueFourDigitNumber();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(4));
            Assert.That(int.TryParse(result, out _), Is.True);
            Assert.That(new HashSet<char>(result), Has.Count.EqualTo(4));
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
            _mockSessionRepository.Setup(repo => repo.EndGame(sessionId)).Returns(Task.CompletedTask);
            _mockResultRepository.Setup(repo => repo.AddGameResult(sessionId, 1, true)).Returns(Task.CompletedTask);

            // Act
            var result = await _gameLogicProcessor.GameFinished(GameConstants.SecretNumberLength, 1, sessionId);

            // Assert
            Assert.That(result, Is.True);
            _mockSessionRepository.Verify(repo => repo.EndGame(sessionId), Times.Once);
            _mockResultRepository.Verify(repo => repo.AddGameResult(sessionId, 1, true), Times.Once);
        }

        [Test]
        public async Task GameFinished_ShouldReturnTrue_WhenMaxAttemptsReached()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            _mockSessionRepository.Setup(repo => repo.EndGame(sessionId)).Returns(Task.CompletedTask);
            _mockResultRepository.Setup(repo => repo.AddGameResult(sessionId, GameConstants.MaxAttempts, false)).Returns(Task.CompletedTask);

            // Act
            var result = await _gameLogicProcessor.GameFinished(0, GameConstants.MaxAttempts, sessionId);

            // Assert
            Assert.That(result, Is.True);
            _mockSessionRepository.Verify(repo => repo.EndGame(sessionId), Times.Once);
            _mockResultRepository.Verify(repo => repo.AddGameResult(sessionId, GameConstants.MaxAttempts, false), Times.Once);
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
            _mockSessionRepository.Verify(repo => repo.EndGame(It.IsAny<Guid>()), Times.Never);
            _mockResultRepository.Verify(repo => repo.AddGameResult(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Never);
        }
    }
}