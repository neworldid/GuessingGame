using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameResultRepository
{
	Task<bool> AddGameResultAndEndSessionAsync(Guid sessionId, int attemptCount, bool won);
	
	Task<GameDetailsModel> GetResultDetailsBySessionId(Guid sessionId);
	
	Task<IEnumerable<GameDetailsModel>> GetGameResults();
}