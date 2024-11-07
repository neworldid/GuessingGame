namespace GuessingGame.Application.Interfaces;

public interface IGameSessionRepository
{
	Task<int> AddGameSession(string playerName, int secretNumber);
}