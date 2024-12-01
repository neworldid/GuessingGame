namespace GuessingGame.Domain.Models;

public class GameUserModel(int id, string username, string password, string email)
{
	public int Id { get; } = id;
	public string Username { get; } = username;
	public string Password { get; } = password;
	public string Email { get; } = email;
}