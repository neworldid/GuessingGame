namespace GuessingGame.Application.Contracts;

public record AttemptResponse()
{
	public int PositionMatch { get; set; }
	public int AttemptDigitsMatchInNumber { get; set; }
}