namespace GuessingGame.Infrastructure.Database.Entities;

public class GameSetting
{
	public int Id { get; set; }
	public string Description { get; set; } = null!;
	public bool IsEnabled { get; set; }
}