using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Database.Repositories;

public class GameResultRepository(GuessingGameDbContext context) : IGameResultRepository
{
	public async Task<bool> AddGameResultAndEndSessionAsync(Guid sessionId, int attemptCount, bool won)
	{
		await using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var gameSession = await context.GameSessions.FirstOrDefaultAsync(s => s.Id == sessionId);
			if (gameSession != null)
			{
				gameSession.EndTime = DateTime.UtcNow;
				
				var result = new GameResult
				{
					GameSessionId = sessionId,
					AttemptCount = attemptCount,
					Won = won,
					Timestamp = DateTime.UtcNow
				};

				await context.GameResults.AddAsync(result);
				await context.SaveChangesAsync();
			}

			await transaction.CommitAsync();
			return true;
		}
		catch (Exception e)
		{
			await transaction.RollbackAsync();
			throw new Exception("An error occurred while adding game result and ending session", e);
		}
		
	}
	
	public async Task<GameDetailsModel> GetResultDetailsBySessionId(Guid sessionId)
	{
		var gameResult = await context.GameResults
			.AsNoTracking()
			.Include(gameResult => gameResult.GameSession)
			.FirstOrDefaultAsync(s => s.GameSessionId == sessionId);

		return new GameDetailsModel
		{
			StartTime = gameResult.GameSession.StartTime,
			EndTime = gameResult.Timestamp,
			SecretNumber = gameResult.GameSession.SecretNumber,
			AttemptCount = gameResult.AttemptCount,
			Won = gameResult.Won
		};
	}

	public async Task<IEnumerable<GameDetailsModel>> GetGameResults()
	{
		var results = await context.GameResults
			.AsNoTracking()
			.Select(r => new GameDetailsModel
			{
				PlayerName = r.GameSession.PlayerName,
				SecretNumber = r.GameSession.SecretNumber,
				AttemptCount = r.AttemptCount,
				StartTime = r.GameSession.StartTime,
				EndTime = r.Timestamp,
				Won = r.Won,
			})
			.ToListAsync();
		
		return results;
	}
}