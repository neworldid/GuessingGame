namespace GuessingGame.DataAccess.Entities;

public class GameSession
{
	public Guid Id { get; set; }
	public string PlayerName { get; set; }
	public string SecretNumber { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime? EndTime { get; set; }
	public bool IsCompleted { get; set; }
	
	public GameResult GameResult { get; set; }
	public ICollection<GameAttempt> GameAttempts { get; set; }
}