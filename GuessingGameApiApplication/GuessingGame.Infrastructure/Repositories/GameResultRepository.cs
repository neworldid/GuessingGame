using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Repositories;

public class GameResultRepository(GuessingGameDbContext context) : IGameResultRepository
{
	public async Task AddGameResult(Guid sessionId, int attemptCount, bool won)
	{
		var result = new GameResult
		{
			GameSessionId = sessionId,
			AttemptCount = attemptCount,
			Won = won,
			Timestamp = DateTime.UtcNow
		};
		
		context.GameResults.Add(result);
		await context.SaveChangesAsync();
	}

	public async Task<IEnumerable<GameDetailsModel>> GetGameResults()
	{
		var results = await context.GameResults
			.AsNoTracking()
			.Select(r => new GameDetailsModel
			{
				GameResultId = r.Id,
				PlayerName = r.GameSession.PlayerName,
				SecretNumber = r.GameSession.SecretNumber,
				AttemptCount = r.AttemptCount,
				StartTime = r.GameSession.StartTime,
				EndTime = r.GameSession.EndTime,
				Won = r.Won,
			})
			.ToListAsync();
		
		return results;
	}

	public async Task<int> DeleteGameResult(int resultId)
	{
		var result = await context.GameResults.FirstOrDefaultAsync(r => r.Id == resultId);
		if (result == null) return 0;
		context.GameResults.Remove(result);
		return await context.SaveChangesAsync();
	}
}