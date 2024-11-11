using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;

namespace GuessingGame.Application.Services;

public class GameResultService(IGameResultRepository gameResultRepository) : IGameResultService
{
	public async Task<IEnumerable<GameResultResponse>?> GetGameResults()
	{
		try
		{
			var result = await gameResultRepository.GetGameResults();
		
			return result.Select(r => new GameResultResponse
			{
				Id = r.Id,
				PlayerName = r.PlayerName,
				SecretNumber = r.SecretNumber,
				AttemptCount = r.AttemptCount,
				Won = r.Won
			});
		}
		catch (Exception e)
		{
			return null;
		}
	}
}