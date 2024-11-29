namespace GuessingGame.Domain.Models;

public class GameUserModel
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
}