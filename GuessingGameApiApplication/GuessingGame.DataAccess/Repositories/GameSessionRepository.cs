using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.DataAccess.Entities;

namespace GuessingGame.DataAccess.Repositories;

public class GameSessionRepository(GuessingGameDbContext context) : IGameSessionRepository
{
	public async Task<int> AddGameSession(string playerName, int secretNumber)
	{
		var gameSession = new GameSession
		{
			PlayerName = playerName,
			SecretNumber = secretNumber,
			StartTime = DateTime.UtcNow
		};
		
		await context.GameSessions.AddAsync(gameSession);
		await context.SaveChangesAsync();
		return gameSession.Id;
	}
}