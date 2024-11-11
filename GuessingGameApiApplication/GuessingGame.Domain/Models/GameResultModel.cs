namespace GuessingGame.Domain.Models;

public class GameResultModel
{
	public int Id { get; set; }
	public string PlayerName { get; set; }
	
	public string SecretNumber { get; set; }
	
	public int AttemptCount { get; set; }
	
	public TimeSpan Duration { get; set; }
	
	public bool Won { get; set; }
}