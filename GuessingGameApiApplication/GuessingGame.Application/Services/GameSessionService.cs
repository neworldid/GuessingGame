using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;

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
		catch
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