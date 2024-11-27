using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;

namespace GuessingGame.Application.Processors;

public class GameLogicProcessor(IGameResultRepository resultRepository) : IGameLogicProcessor
{
	public (int positionMatch, int matchInIncorrectPositions) CalculateMatches(string secretNumber, string guessedNumber)
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

	public async Task<bool> GameFinished(int positionMatch, int attemptNumber, Guid sessionId)
	{
		if (positionMatch != GameConstants.SecretNumberLength &&
		    attemptNumber != GameConstants.MaxAttempts)
			return false;
		
		return await resultRepository.AddGameResultAndEndSessionAsync(sessionId, attemptNumber, positionMatch == GameConstants.SecretNumberLength);
	}
}