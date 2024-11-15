using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;

namespace GuessingGame.Application.Processors;

public class GameLogicProcessor(IGameSessionRepository sessionRepository, IGameResultRepository resultRepository) : IGameLogicProcessor
{
	public string GenerateUniqueFourDigitNumber()
	{
		var random = new Random();
		var digits = new HashSet<int>();

		while (digits.Count < 4)
		{
			var newDigit = random.Next(0, 10);
			if (!digits.Contains(newDigit))
			{
				digits.Add(random.Next(0, 10));
			}
		}

		return string.Join("", digits);
	}

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
		
		await sessionRepository.EndGame(sessionId);
		await resultRepository.AddGameResult(sessionId, attemptNumber, positionMatch == GameConstants.SecretNumberLength);
		return true;

	}
}