namespace GuessingGame.API.Contracts;

public record GameSettingRequest
{
	public int Id { get; init; }
	public bool IsEnabled { get; init; }
}