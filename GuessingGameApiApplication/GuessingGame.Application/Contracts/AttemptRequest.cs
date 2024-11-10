namespace GuessingGame.Application.Contracts;

public record AttemptRequest(string Number, Guid SessionId);