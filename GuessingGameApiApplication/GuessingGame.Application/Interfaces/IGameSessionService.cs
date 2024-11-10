namespace GuessingGame.Application.Interfaces;

public interface IGameSessionService
{
	Task<Guid?> StartNewGame(string playerName);
}