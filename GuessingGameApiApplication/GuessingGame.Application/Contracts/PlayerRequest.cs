using System.ComponentModel.DataAnnotations;

namespace GuessingGame.Application.Contracts;

public record PlayerRequest
{
	[Required]
	[MinLength(2)]
	public string PlayerName { get; init; } = string.Empty;
}