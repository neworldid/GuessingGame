using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions;

public interface IGameSessionRepository
{
	Task<Guid?> AddGameSession(string playerName, string secretNumber);
	
	Task<GameSessionModel> GetGameDetails(Guid sessionId);

	Task EndGame(Guid sessionId);
}