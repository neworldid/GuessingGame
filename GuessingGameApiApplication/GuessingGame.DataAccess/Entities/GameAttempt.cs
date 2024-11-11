namespace GuessingGame.DataAccess.Entities;

public class GameAttempt
{
	public int Id { get; set; }
	public Guid GameSessionId { get; set; }
	public int AttemptNumber { get; set; }
	public string GuessedNumber { get; set; } = string.Empty;
	public int PositionMatch { get; set; }
	public int AttemptDigitsMatchInNumber { get; set; }
	public DateTime AttemptTime { get; set; }

	public GameSession GameSession { get; set; }
}