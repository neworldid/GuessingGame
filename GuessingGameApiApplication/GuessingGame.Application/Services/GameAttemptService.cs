using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;

namespace GuessingGame.Application.Services;

public class GameAttemptService(IGameSessionRepository sessionRepository, IGameAttemptRepository attemptRepository, IGameResultRepository gameResultRepository) : IGameAttemptService
{
	public async Task<AttemptResponse?> ProcessAttemptAsync(AttemptRequest request)
	{
		try
		{
			var game = await sessionRepository.GetGameDetails(request.SessionId);
			var secretNumber = game.SecretNumber;

			var (positionMatch, matchInIncorrectPositions) = CalculateMatches(secretNumber, request.Number);

			var attemptNumber = game.AttemptCount + 1;
			var attemptModel = new GameAttemptModel(request.SessionId, request.Number, positionMatch, matchInIncorrectPositions, attemptNumber);
		
			await attemptRepository.AddAttempt(attemptModel);

			var gameFinished = await GameFinished(matchInIncorrectPositions, attemptNumber, request.SessionId);
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
				AttemptNumber = attemptNumber + 1
			};
		}
		catch (Exception e)
		{
			return null;
		}
		
	}
	
	private static (int positionMatch, int matchInIncorrectPositions) CalculateMatches(string secretNumber, string guessedNumber)
	{
		var positionMatch = 0;
		var matchInIncorrectPositions = 0;

		for (var i = 0; i < secretNumber.Length; i++)
		{
			if (secretNumber[i] == guessedNumber[i])
			{
				positionMatch++;
			}
			else if (secretNumber.Contains(guessedNumber[i]) && secretNumber[i] != guessedNumber[i])
			{
				matchInIncorrectPositions++;
			}
		}

		return (positionMatch, matchInIncorrectPositions);
	}
	
	private async Task<bool> GameFinished(int matchInIncorrectPositions, int attemptNumber, Guid sessionId)
	{
		var isGameFinished = false;
		if (matchInIncorrectPositions == GameConstants.SecretNumberLength)
		{
			await sessionRepository.EndGame(sessionId);
			await gameResultRepository.AddGameResult(sessionId, attemptNumber, true);
			isGameFinished = true;
		}
		else if(attemptNumber == GameConstants.MaxAttempts)
		{
			await sessionRepository.EndGame(sessionId);
			await gameResultRepository.AddGameResult(sessionId, GameConstants.MaxAttempts, false);
			isGameFinished = true;
		}

		return isGameFinished;
	}
}