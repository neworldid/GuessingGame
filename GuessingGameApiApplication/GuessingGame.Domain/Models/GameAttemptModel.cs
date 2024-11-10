namespace GuessingGame.Domain.Models;

public class GameAttemptModel
{
	public int Id { get; set; }
	public Guid GameSessionId { get; set; }
	
	// The number that the user guessed
	public string GuessedNumber { get; set; }
	public int AttemptNumber { get; set; }
	public int PositionMatch { get; set; }
	public int AttemptDigitsMatchInNumber { get; set; }
}