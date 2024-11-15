using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameAttemptRepository
{
	Task<int> AddAttempt(GameAttemptModel model);
}