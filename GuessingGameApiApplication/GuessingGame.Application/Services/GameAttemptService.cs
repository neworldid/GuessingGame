using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;
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
			var game = await sessionRepository.GetGameDetails(request.SessionId);
			var secretNumber = game.SecretNumber;

			var (positionMatch, matchInIncorrectPositions) = logicProcessor.CalculateMatches(secretNumber, request.Number);

			var attemptNumber = game.AttemptCount + 1;
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
		catch (Exception e)
		{
			return null;
		}
	}
}