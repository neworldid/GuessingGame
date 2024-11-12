namespace GuessingGame.Domain.Models;

public class GameAttemptModel(
	Guid gameSessionId,
	string guessedNumber,
	int positionMatch,
	int matchInIncorrectPositions,
	int attemptNumber)
{
	public Guid GameSessionId { get; } = gameSessionId;

	// The number that the user guessed
	public string GuessedNumber { get; } = guessedNumber;
	public int AttemptNumber { get; } = attemptNumber;
	public int PositionMatch { get; } = positionMatch;
	public int MatchInIncorrectPositions { get; } = matchInIncorrectPositions;
}