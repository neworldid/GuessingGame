using GuessingGame.DataAccess.Entities;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.DataAccess.Repositories;

public class GameSessionRepository(GuessingGameDbContext context) : IGameSessionRepository
{
	public async Task<Guid?> AddGameSession(string playerName, string secretNumber)
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
	
	public async Task<GameDetailsModel> GetGameDetails(Guid sessionId)
	{
		var gameSession = await context.GameSessions
			.AsNoTracking()
			.Include(gameSession => gameSession.GameAttempts)
			.FirstOrDefaultAsync(s => s.Id == sessionId);

		return new GameDetailsModel
		{
			PlayerName = gameSession.PlayerName,
			StartTime = gameSession.StartTime,
			EndTime = gameSession.EndTime,
			SecretNumber = gameSession.SecretNumber,
			AttemptCount = gameSession.GameAttempts.Count,
			Won = gameSession.GameAttempts.Any(a => a.PositionMatch == GameConstants.SecretNumberLength)
		};
	}
	
	public async Task EndGame(Guid sessionId)
	{
		var gameSession = await context.GameSessions.FirstOrDefaultAsync(s => s.Id == sessionId);

		if (gameSession != null) 
			gameSession.EndTime = DateTime.UtcNow;
		
		await context.SaveChangesAsync();
	}
}