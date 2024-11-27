using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Database.Repositories;

public class GameAttemptRepository(GuessingGameDbContext context) : IGameAttemptRepository
{
	public async Task<int> AddAttempt(GameAttemptModel model)
	{
		var entity = new GameAttempt
		{
			GameSessionId = model.GameSessionId,
			AttemptNumber = model.AttemptNumber,
			GuessedNumber = model.GuessedNumber,
			PositionMatch = model.PositionMatch,
			AttemptDigitsMatchInNumber = model.MatchInIncorrectPositions,
			AttemptTime = DateTime.UtcNow
		};
		
		context.GameAttempts.Add(entity);
		return await context.SaveChangesAsync();
	}

	public async Task<IEnumerable<GameAttemptModel>> GetAttempts(Guid sessionId)
	{
		var attempts = await context.GameAttempts
			.Where(x => x.GameSessionId == sessionId)
			.Select(x => new GameAttemptModel(
				x.GameSessionId,
				x.GuessedNumber,
				x.PositionMatch,
				x.AttemptDigitsMatchInNumber,
				x.AttemptNumber))
			.ToListAsync();
		
		return attempts;
	}

	public async Task<int> GetTotalAttempts(Guid sessionId)
	{
		return await context.GameAttempts
			.CountAsync(x => x.GameSessionId == sessionId);
	}
}