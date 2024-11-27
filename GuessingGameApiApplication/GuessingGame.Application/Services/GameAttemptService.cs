using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;

namespace GuessingGame.Application.Services;

public class GameAttemptService(
	IGameSessionRepository sessionRepository, 
	IGameAttemptRepository attemptRepository, 
	IGameLogicProcessor logicProcessor) : IGameAttemptService
{
	public async Task<AttemptResponse?> ProcessAttemptAsync(AttemptRequest request)
	{
		try
		{
			var secretNumber = await sessionRepository.GetSecretNumber(request.SessionId);

			var (positionMatch, matchInIncorrectPositions) = logicProcessor.CalculateMatches(secretNumber, request.Number);

			var attemptNumber = await attemptRepository.GetTotalAttempts(request.SessionId);
			attemptNumber++;
			var attemptModel = new GameAttemptModel(request.SessionId, request.Number, positionMatch, matchInIncorrectPositions, attemptNumber);
		
			await attemptRepository.AddAttempt(attemptModel);

			var gameFinished = await logicProcessor.GameFinished(positionMatch, attemptNumber, request.SessionId);
			if (gameFinished)
			{
				return new AttemptResponse
				{
					IsCompleted = true
				};
			}

			return new AttemptResponse
			{
				PositionMatch = positionMatch,
				MatchInIncorrectPositions = matchInIncorrectPositions,
				TriesLeft = GameConstants.MaxAttempts - attemptNumber
			};
		}
		catch
		{
			return null;
		}
	}
}