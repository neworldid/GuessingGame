using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Database.Repositories;

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
	
	public async Task<string?> GetSecretNumber(Guid sessionId)
	{
		return await context.GameSessions
			.AsNoTracking()
			.Where(s => s.Id == sessionId)
			.Select(s => s.SecretNumber)
			.FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<GameSessionDetails>> GetAllGameSessions()
	{
		var gameSessions = await context.GameSessions
			.AsNoTracking()
			.Include(gameSession => gameSession.GameAttempts)
			.ToListAsync();

		var gameSessionDetails = gameSessions.Select(gameSession => new GameSessionDetails
		{
			Id = gameSession.Id,
			PlayerName = gameSession.PlayerName,
			StartTime = gameSession.StartTime,
			EndTime = gameSession.EndTime,
			SecretNumber = gameSession.SecretNumber,
			AttemptCount = gameSession.GameAttempts?.Count ?? 0,
			Won = gameSession.GameAttempts?.Any(a => a.PositionMatch == GameConstants.SecretNumberLength) ?? false
		});

		return gameSessionDetails;
	}

	public async Task<bool> DeleteSessionsAsync(Guid sessionId)
	{
		await using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var gameSession = await context.GameSessions
				.Include(gs => gs.GameAttempts)
				.Include(gs => gs.GameResult)
				.FirstOrDefaultAsync(gs => gs.Id == sessionId);

			if (gameSession != null)
			{
				if (gameSession.GameAttempts != null)
				{
					context.GameAttempts.RemoveRange(gameSession.GameAttempts);
				}
				
				if (gameSession.GameResult != null)
				{
					context.GameResults.Remove(gameSession.GameResult);
				}
				context.GameSessions.Remove(gameSession);
				await context.SaveChangesAsync();
			}

			await transaction.CommitAsync();
			return true;
		}
		catch
		{
			await transaction.RollbackAsync();
			return false;
		}
	}
}