namespace GuessingGame.Application.Contracts;

public record GameDetailsResponse
{
	public string SecretNumber { get; set; } = string.Empty;
	
	public int AttemptCount { get; set; }
	public TimeSpan? Duration { get; set; }
	
	public bool Won { get; set; }
}