using System.ComponentModel.DataAnnotations;
using GuessingGame.Domain.Constants;

namespace GuessingGame.Domain.Models;

public record AttemptRequest
{
	[Required]
	[StringLength(
		GameConstants.SecretNumberLength, 
		MinimumLength = GameConstants.SecretNumberLength, 
		ErrorMessage = "Number must be exactly 4 digits long."
		)]
	public string Number { get; init; } = string.Empty;

	[Required]
	public Guid SessionId { get; init; }
};