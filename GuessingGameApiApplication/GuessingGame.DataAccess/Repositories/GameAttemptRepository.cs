using GuessingGame.DataAccess.Entities;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Models;

namespace GuessingGame.DataAccess.Repositories;

public class GameAttemptRepository(GuessingGameDbContext context) : IGameAttemptRepository
{
	public async Task<int> AddAttempt(GameAttemptModel model)
	{
		try
		{
			var entity = new GameAttempt
			{
				GameSessionId = model.GameSessionId,
				AttemptNumber = model.AttemptNumber,
				GuessedNumber = model.GuessedNumber,
				PositionMatch = model.PositionMatch,
				AttemptDigitsMatchInNumber = model.AttemptDigitsMatchInNumber,
				AttemptTime = DateTime.UtcNow
			};
		
			context.GameAttempts.Add(entity);
			return await context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return 0;
		}
	}
}