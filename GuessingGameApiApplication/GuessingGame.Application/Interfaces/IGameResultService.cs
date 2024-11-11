using GuessingGame.Application.Contracts;

namespace GuessingGame.Application.Interfaces;

public interface IGameResultService
{
	Task<IEnumerable<GameResultResponse>?> GetGameResults();
}