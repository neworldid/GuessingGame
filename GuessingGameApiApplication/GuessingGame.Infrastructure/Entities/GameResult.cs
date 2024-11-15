namespace GuessingGame.Infrastructure.Entities;

public class GameResult
{
	public int Id { get; set; }
	public Guid GameSessionId { get; set; }
	public bool Won { get; set; }
	public int AttemptCount { get; set; }
	public DateTime Timestamp { get; set; }

	public GameSession GameSession { get; set; } = default!;
}