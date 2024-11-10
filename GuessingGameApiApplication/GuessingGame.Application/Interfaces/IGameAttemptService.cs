using GuessingGame.Application.Contracts;

namespace GuessingGame.Application.Interfaces;

public interface IGameAttemptService
{
	Task<AttemptResponse> ProcessAttemptAsync(AttemptRequest request);
}