using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Entities;

namespace GuessingGame.Infrastructure.Repositories;

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
}