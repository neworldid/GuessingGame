using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Services;

public interface IGameResultService
{
	Task<IEnumerable<GameResultResponse>?> GetGameResults();
	
	Task<GameResultResponse?> GetGameDetailsBySessionId(Guid sessionId);
}