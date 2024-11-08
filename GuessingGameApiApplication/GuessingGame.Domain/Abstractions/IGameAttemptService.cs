namespace GuessingGame.Domain.Abstractions;

public interface IGameAttemptService
{
	Task<int> ProcessAttemptAsync(int sessionId, int number);
}