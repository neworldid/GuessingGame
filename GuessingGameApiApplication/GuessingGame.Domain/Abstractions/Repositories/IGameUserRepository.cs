using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameUserRepository
{
	Task<int> AddUser(string username, string password, string email);
	
	Task<GameUserModel?> GetUserByEmail(string email);
}