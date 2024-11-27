using System.ComponentModel.DataAnnotations;

namespace GuessingGame.Domain.Models;

public class GameSessionDetails
{
	public Guid Id { get; set; }
	public string PlayerName { get; set; } = string.Empty;
	
	public string SecretNumber { get; set; } = string.Empty;
	
	public int AttemptCount { get; set; }
	
	public DateTime StartTime { get; set; }
	
	public DateTime? EndTime { get; set; }
	
	public bool Won { get; set; }
}