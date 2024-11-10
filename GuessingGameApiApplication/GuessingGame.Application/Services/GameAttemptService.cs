using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Models;

namespace GuessingGame.Application.Services;

public class GameAttemptService(IGameSessionRepository sessionRepository, IGameAttemptRepository attemptRepository) : IGameAttemptService
{
	public async Task<AttemptResponse> ProcessAttemptAsync(AttemptRequest request)
	{
		var game = await sessionRepository.GetGameDetails(request.SessionId);
		var positionMatch = 0;
		var attemptDigitsMatchInNumber = 0;

		var secretNumber = game.SecretNumber;
		
		for (var i = 0; i < secretNumber.Length; i++)
		{
			if (secretNumber[i] == request.Number[i])
			{
				positionMatch++;
			}
			else if (secretNumber.Contains(request.Number[i]))
			{
				attemptDigitsMatchInNumber++;
			}
		}

		var attemptModel = new GameAttemptModel
		{
			GameSessionId = request.SessionId,
			GuessedNumber = request.Number,
			PositionMatch = positionMatch,
			AttemptDigitsMatchInNumber = attemptDigitsMatchInNumber,
			AttemptNumber = game.AttemptCount + 1
		};
		
		await attemptRepository.AddAttempt(attemptModel);

		return new AttemptResponse
		{
			PositionMatch = positionMatch,
			AttemptDigitsMatchInNumber = attemptDigitsMatchInNumber
		};
	}
}