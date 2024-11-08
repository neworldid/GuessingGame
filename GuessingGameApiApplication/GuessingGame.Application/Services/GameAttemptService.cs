using GuessingGame.Domain.Abstractions;

namespace GuessingGame.Application.Services;

public class GameAttemptService : IGameAttemptService
{
	public Task<int> ProcessAttemptAsync(int sessionId, int number)
	{
		throw new NotImplementedException();
	}
}