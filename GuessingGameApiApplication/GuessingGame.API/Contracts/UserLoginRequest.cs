using System.ComponentModel.DataAnnotations;

namespace GuessingGame.API.Contracts;

public record UserLoginRequest
{
	[Required]
	public string Email { get; init; } = string.Empty;
	
	[Required]
	public string Password { get; init; } = string.Empty;
}