using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;

namespace GuessingGame.Application.Services;

public class GameSessionService(IGameSessionRepository sessionRepository) : IGameSessionService
{
	public async Task<int> StartNewGame(string playerName)
	{
		var secretNumber = new Random().Next(1, 10000);
		await sessionRepository.AddGameSession(playerName, secretNumber);
		return secretNumber;
	}
}