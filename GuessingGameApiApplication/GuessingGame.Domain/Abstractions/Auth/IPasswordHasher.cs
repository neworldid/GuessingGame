namespace GuessingGame.Domain.Abstractions.Auth;

public interface IPasswordHasher
{
	string Generate(string password);
	bool Verify(string password, string hashedPassword);
}