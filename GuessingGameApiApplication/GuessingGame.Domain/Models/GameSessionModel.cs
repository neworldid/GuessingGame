namespace GuessingGame.Domain.Models;

public class GameSessionModel
{
	public string SecretNumber { get; set; }

	public int AttemptCount;
	
	public DateTime StartTime { get; set; }
	
	public DateTime? EndTime { get; set; }
	
	public bool Won { get; set; }
}