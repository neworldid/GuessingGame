using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;

namespace GuessingGame.Application.Processors;

public class SessionCleanupProcessor(IGameSessionRepository sessionRepository) : ISessionCleanupProcessor
{
	public async Task CleanupSessionsAsync()
	{
		var allGameSessions = await sessionRepository.GetAllGameSessions();
		var sessionsToDelete = allGameSessions
			.Where(x => x.StartTime < DateTime.Now.AddDays(-2) &&
			            (x.EndTime == null || x.Won == false))
			.Select(x => x.SessionId)
			.ToList();

		foreach (var sessionId in sessionsToDelete)
		{
			await sessionRepository.DeleteSessionsAsync(sessionId);
		}
	}
}