using System.ComponentModel.DataAnnotations;

namespace GuessingGame.API.Contracts;

public record UserRegisterRequest
{
	[Required]
	public string Username { get; init; } = string.Empty;
	
	[Required]
	[EmailAddress]
	public string Email { get; init; } = string.Empty;
	
	[Required]
	[MinLength(5)]
	public string Password { get; init; } = string.Empty;
}