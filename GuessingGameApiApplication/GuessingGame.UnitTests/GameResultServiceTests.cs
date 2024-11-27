using Moq;
using GuessingGame.Application.Services;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;

namespace GuessingGame.UnitTests;

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

	[Test]
	public async Task GetGameDetails_ShouldReturnGameResultResponse_WhenGameExists()
	{
		// Arrange
		var game = new GameDetailsModel
		{
			SecretNumber = "1234",
			AttemptCount = 5,
			StartTime = DateTime.Now,
			Won = true
		};
		game.EndTime = game.StartTime.AddMinutes(5);
		
		var guid = Guid.NewGuid();
		_mockGameResultRepository.Setup(repo => repo.GetResultDetailsBySessionId(guid)).ReturnsAsync(game);

		// Act
		var result = await _gameResultService.GetGameDetailsBySessionId(guid);

		// Assert
		_mockGameResultRepository.Verify(repo => repo.GetResultDetailsBySessionId(guid), Times.Once);
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
		var guid = Guid.NewGuid();
		_mockGameResultRepository.Setup(repo => repo.GetResultDetailsBySessionId(guid)).Throws(new Exception());

		// Act
		var result = await _gameResultService.GetGameDetailsBySessionId(guid);

		// Assert
		_mockGameResultRepository.Verify(repo => repo.GetResultDetailsBySessionId(guid), Times.Once);
		Assert.That(result, Is.Null);
	}
}