namespace GuessingGame.Domain.Abstractions.Services;

public interface IGameUserService
{
	Task<int> LoginUser(string email, string password);
	Task<int> RegisterUser(string username, string email, string password);
}