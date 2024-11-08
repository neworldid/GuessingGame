namespace GuessingGame.Domain.Abstractions;

public interface IGameSessionRepository
{
	Task<int> AddGameSession(string playerName, int secretNumber);
}