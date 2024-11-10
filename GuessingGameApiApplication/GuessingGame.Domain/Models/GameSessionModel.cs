namespace GuessingGame.Domain.Models;

public class GameSessionModel
{
	public string SecretNumber { get; set; }

	public int AttemptCount;
}