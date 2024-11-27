namespace GuessingGame.Domain.Abstractions.Processors;

public interface ISessionCleanupProcessor
{
	Task CleanupSessionsAsync();
}