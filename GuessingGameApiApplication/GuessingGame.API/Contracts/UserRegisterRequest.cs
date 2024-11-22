using System.ComponentModel.DataAnnotations;

namespace GuessingGame.API.Contracts;

public record UserRegisterRequest
{
	[Required]
	public string Username { get; init; }
	
	[Required]
	[EmailAddress]
	public string Email { get; init; }
	
	[Required]
	public string Password { get; init; }
}