namespace GuessingGame.Application.Contracts;

public record AttemptResponse
{
	public int PositionMatch { get; set; }
	public int MatchInIncorrectPositions { get; set; }
	
	public int AttemptNumber { get; set; }
	
	public bool IsCompleted { get; set; }
}