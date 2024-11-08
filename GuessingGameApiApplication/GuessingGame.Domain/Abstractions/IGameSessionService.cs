namespace GuessingGame.Domain.Abstractions;

public interface IGameSessionService
{
	Task<int> StartNewGame(string playerName);
}