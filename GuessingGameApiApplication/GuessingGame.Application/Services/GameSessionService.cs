using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;

namespace GuessingGame.Application.Services;

public class GameSessionService(IGameSessionRepository sessionRepository, IGameLogicProcessor logicProcessor) : IGameSessionService
{
	public async Task<Guid?> StartNewGame(string playerName)
	{
		try
		{
			var secretNumber = logicProcessor.GenerateUniqueFourDigitNumber();
			return await sessionRepository.AddGameSession(playerName, secretNumber);
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public async Task<GameResultResponse?> GetGameDetails(Guid sessionId)
	{
		try
		{
			var game = await sessionRepository.GetGameDetails(sessionId);
			var duration = game.EndTime - game.StartTime;
			var durationString = duration?.ToString(@"mm\:ss");
		
			return new GameResultResponse
			{
				PlayerName = game.PlayerName,
				SecretNumber = game.SecretNumber,
				AttemptCount = game.AttemptCount,
				Duration = durationString,
				Won = game.Won
			};
		}
		catch (Exception e)
		{
			return null;
		}
	}
}