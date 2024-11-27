using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;
using GuessingGame.Domain.Models;

namespace GuessingGame.Application.Services;

public class GameResultService(IGameResultRepository resultRepository) : IGameResultService
{
	public async Task<IEnumerable<GameResultResponse>?> GetGameResults()
	{
		try
		{
			var result = await resultRepository.GetGameResults();
		
			return result.Select(r => new GameResultResponse
			{
				PlayerName = r.PlayerName,
				SecretNumber = r.SecretNumber,
				AttemptCount = r.AttemptCount,
				Duration = GetDurationString(r.StartTime, r.EndTime),
				Won = r.Won
			});
		}
		catch
		{
			return null;
		}
	}

	public async Task<GameResultResponse?> GetGameDetailsBySessionId(Guid sessionId)
	{
		try
		{
			var game = await resultRepository.GetResultDetailsBySessionId(sessionId);
		
			return new GameResultResponse
			{
				SecretNumber = game.SecretNumber,
				AttemptCount = game.AttemptCount,
				Duration = GetDurationString(game.StartTime, game.EndTime),
				Won = game.Won
			};
		}
		catch
		{
			return null;
		}
	}
	
	private static string? GetDurationString(DateTime startTime, DateTime? endTime)
	{
		var duration = endTime - startTime;
		return duration?.ToString(@"mm\:ss");
	}
}