namespace GuessingGame.DataAccess.Entities;

public class GameAttempt
{
	public int Id { get; set; }
	public int GameSessionId { get; set; }
	public int AttemptNumber { get; set; }
	public int GuessedNumber { get; set; }
	public DateTime AttemptTime { get; set; }

	public GameSession GameSession { get; set; }
}