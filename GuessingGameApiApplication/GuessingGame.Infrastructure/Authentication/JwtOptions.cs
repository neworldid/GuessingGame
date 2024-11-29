namespace GuessingGame.Infrastructure.Authentication;

public class JwtOptions
{
	public string SecretKey { get; set; } = string.Empty;
	
	public int ExpiryHours { get; set; }
	
	public string Issuer { get; set; } = string.Empty;
}