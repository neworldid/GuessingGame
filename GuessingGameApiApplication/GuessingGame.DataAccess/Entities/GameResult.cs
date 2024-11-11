namespace GuessingGame.DataAccess.Entities;

public class GameResult
{
	public int Id { get; set; }
	public Guid GameSessionId { get; set; }
	public bool Won { get; set; }
	public int AttemptCount { get; set; }
	public DateTime Timestamp { get; set; }

	// Navigation property for the related GameSession
	public GameSession GameSession { get; set; }
}