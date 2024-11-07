namespace GuessingGame.Application.Interfaces;

public interface IGameSessionService
{
	Task<int> StartNewGame(string playerName);
}