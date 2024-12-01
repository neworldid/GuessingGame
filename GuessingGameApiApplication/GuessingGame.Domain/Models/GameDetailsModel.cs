namespace GuessingGame.Domain.Models;

public class GameDetailsModel
{
	public Guid SessionId { get; set; }
	
	public string PlayerName { get; set; } = string.Empty;
	
	public string SecretNumber { get; set; } = string.Empty;
	
	public int AttemptCount { get; set; }
	
	public DateTime StartTime { get; set; }
	
	public DateTime? EndTime { get; set; }
	
	public bool Won { get; set; }
}