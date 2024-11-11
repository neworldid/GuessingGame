using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;

namespace GuessingGame.Application.Services;

public class GameSessionService(IGameSessionRepository sessionRepository) : IGameSessionService
{
	public async Task<Guid?> StartNewGame(string playerName)
	{
		try
		{
			var secretNumber = GenerateUniqueFourDigitNumber();
			return await sessionRepository.AddGameSession(playerName, secretNumber);
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public async Task<GameDetailsResponse?> GetGameDetails(Guid sessionId)
	{
		try
		{
			var game = await sessionRepository.GetGameDetails(sessionId);
			var duration = game.EndTime - game.StartTime;
		
			return new GameDetailsResponse
			{
				SecretNumber = game.SecretNumber,
				AttemptCount = game.AttemptCount,
				Duration = duration,
				Won = game.Won
			};
		}
		catch (Exception e)
		{
			return null;
		}
	}

	private static string GenerateUniqueFourDigitNumber()
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
}