namespace GuessingGame.Application.Interfaces;

public interface IGameAttemptService
{
	Task<int> ProcessAttemptAsync(int sessionId, int number);
}