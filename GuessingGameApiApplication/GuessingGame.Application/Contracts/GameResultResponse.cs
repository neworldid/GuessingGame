namespace GuessingGame.Application.Contracts;

public record GameResultResponse
{
	public int Id { get; set; }
	public string PlayerName { get; set; } = string.Empty;
	
	public string SecretNumber { get; set; } = string.Empty;
	
	public int AttemptCount { get; set; }
	
	public string? Duration { get; set; }
	
	public bool Won { get; set; }
}