using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;

namespace GuessingGame.Application.Services;

public class GameSessionService(IGameSessionRepository sessionRepository) : IGameSessionService
{
	public async Task<Guid?> StartNewGame(string playerName)
	{
		var secretNumber = GenerateUniqueFourDigitNumber();
		return await sessionRepository.AddGameSession(playerName, secretNumber);
	}
	
	private static string GenerateUniqueFourDigitNumber()
	{
		var random = new Random();
		var digits = new HashSet<int>();

		while (digits.Count < 4)
		{
			digits.Add(random.Next(0, 10));
		}

		return string.Join("", digits);
	}
}