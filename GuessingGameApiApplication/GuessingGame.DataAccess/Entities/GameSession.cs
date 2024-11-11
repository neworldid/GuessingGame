namespace GuessingGame.DataAccess.Entities;

public class GameSession
{
	public Guid Id { get; set; }
	public string PlayerName { get; set; } = string.Empty;
	public string SecretNumber { get; set; } = string.Empty;
	public DateTime StartTime { get; set; }
	public DateTime? EndTime { get; set; }
	public GameResult GameResult { get; set; }
	public ICollection<GameAttempt> GameAttempts { get; set; }
}