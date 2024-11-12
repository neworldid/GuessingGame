namespace GuessingGame.Domain.Models;

public class GameDetailsModel
{
	public int GameResultId { get; set; }
	public string PlayerName { get; set; }
	
	public string SecretNumber { get; set; }
	
	public int AttemptCount { get; set; }
	
	public DateTime StartTime { get; set; }
	
	public DateTime? EndTime { get; set; }
	
	public bool Won { get; set; }
}