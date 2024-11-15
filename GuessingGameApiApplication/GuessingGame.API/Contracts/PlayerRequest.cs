using System.ComponentModel.DataAnnotations;

namespace GuessingGame.API.Contracts;

public record PlayerRequest
{
	[Required]
	[MinLength(2)]
	[MaxLength(50)]
	public string PlayerName { get; init; } = string.Empty;
}