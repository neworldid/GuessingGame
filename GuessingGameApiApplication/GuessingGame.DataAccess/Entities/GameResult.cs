namespace GuessingGame.DataAccess.Entities;

public class GameResult
{
	public int Id { get; set; }
	public int GameSessionId { get; set; }
	public string Result { get; set; }
	public DateTime Timestamp { get; set; }

	// Navigation property for the related GameSession
	public GameSession GameSession { get; set; }
}