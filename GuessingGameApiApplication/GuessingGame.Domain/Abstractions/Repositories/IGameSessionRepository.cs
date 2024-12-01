using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameSessionRepository
{
	Task<Guid?> AddGameSession(string playerName, string secretNumber);
	
	Task<string?> GetSecretNumber(Guid sessionId);
	
	Task<IEnumerable<GameDetailsModel>> GetAllGameSessions();
	
	Task<bool> DeleteSessionsAsync(Guid sessionId);
}