using GuessingGame.Application.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using Moq;

namespace GuessingGame.UnitTests.Processors
{
    public class SessionCleanupProcessorTests
    {
        private Mock<IGameSessionRepository> _mockSessionRepository;
        private SessionCleanupProcessor _sessionCleanupProcessor;

        [SetUp]
        public void Setup()
        {
            _mockSessionRepository = new Mock<IGameSessionRepository>();
            _sessionCleanupProcessor = new SessionCleanupProcessor(_mockSessionRepository.Object);
        }

        [Test]
        public async Task CleanupSessionsAsync_ShouldDeleteOldUnfinishedSessions()
        {
            // Arrange
            var oldDate = DateTime.Now.AddDays(-3);
            var recentDate = DateTime.Now.AddDays(-1);
            var gameSessions = new List<GameDetailsModel>
            {
                new() { SessionId = Guid.NewGuid(), StartTime = oldDate, EndTime = null, Won = false },
                new() { SessionId = Guid.NewGuid(), StartTime = oldDate, EndTime = oldDate.AddHours(1), Won = false },
                new() { SessionId = Guid.NewGuid(), StartTime = recentDate, EndTime = null, Won = false },
                new() { SessionId = Guid.NewGuid(), StartTime = oldDate, EndTime = oldDate.AddHours(1), Won = true }
            };

            _mockSessionRepository.Setup(repo => repo.GetAllGameSessions()).ReturnsAsync(gameSessions);

            // Act
            await _sessionCleanupProcessor.CleanupSessionsAsync();

            // Assert
            _mockSessionRepository.Verify(repo => repo.DeleteSessionsAsync(It.IsAny<Guid>()), Times.Exactly(2));
            _mockSessionRepository.Verify(repo => repo.DeleteSessionsAsync(gameSessions[0].SessionId), Times.Once);
            _mockSessionRepository.Verify(repo => repo.DeleteSessionsAsync(gameSessions[1].SessionId), Times.Once);
            _mockSessionRepository.Verify(repo => repo.DeleteSessionsAsync(gameSessions[2].SessionId), Times.Never);
            _mockSessionRepository.Verify(repo => repo.DeleteSessionsAsync(gameSessions[3].SessionId), Times.Never);
        }
    }
}