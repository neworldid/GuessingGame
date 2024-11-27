using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameAttemptRepository
{
	Task<int> AddAttempt(GameAttemptModel model);
	
	Task<IEnumerable<GameAttemptModel>> GetAttempts(Guid sessionId);
	
	Task<int> GetTotalAttempts(Guid sessionId);
}