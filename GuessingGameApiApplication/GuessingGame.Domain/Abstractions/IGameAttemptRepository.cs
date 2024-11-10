using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions;

public interface IGameAttemptRepository
{
	Task<int> AddAttempt(GameAttemptModel model);
}