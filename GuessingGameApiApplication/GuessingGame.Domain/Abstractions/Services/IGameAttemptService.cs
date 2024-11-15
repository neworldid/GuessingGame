using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Services;

public interface IGameAttemptService
{
	Task<AttemptResponse?> ProcessAttemptAsync(AttemptRequest request);
}