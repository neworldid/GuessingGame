using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Services;

public interface IGameSessionService
{
	Task<Guid?> StartNewGame(string playerName);

	Task<GameResultResponse?> GetGameDetails(Guid sessionId);
}