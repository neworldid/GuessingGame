using GuessingGame.DataAccess.Entities;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.DataAccess.Repositories;

public class GameSessionRepository(GuessingGameDbContext context) : IGameSessionRepository
{
	public async Task<Guid?> AddGameSession(string playerName, string secretNumber)
	{
		try
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
		catch (Exception e)
		{
			Console.WriteLine(e);
			return null;
		}
	}
	
	public async Task<GameSessionModel> GetGameDetails(Guid sessionId)
	{
		var gameSession = await context.GameSessions
			.AsNoTracking().Include(gameSession => gameSession.GameAttempts)
			.FirstOrDefaultAsync(s => s.Id == sessionId);

		return new GameSessionModel
		{
			SecretNumber = gameSession.SecretNumber,
			AttemptCount = gameSession.GameAttempts.Count
		};
	}
}