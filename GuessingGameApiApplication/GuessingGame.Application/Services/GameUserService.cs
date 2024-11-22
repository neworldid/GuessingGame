using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;

namespace GuessingGame.Application.Services;

public class GameUserService(IGameUserRepository gameUserRepository, IPasswordHasher passwordHasher) : IGameUserService
{
	public async Task<int> LoginUser(string email, string password)
	{
		var user = await gameUserRepository.GetUserByEmail(email);
		if (user == null)
		{
			return 0;
		}
		
		var result = passwordHasher.Verify(password, user.Password);
		return result == false ? 0 : 1;
	}

	public async Task<int> RegisterUser(string username, string email, string password)
	{
		var user = await gameUserRepository.GetUserByEmail(email);
		if (user != null)
		{
			return 0;
		}

		var hashedPassword = passwordHasher.Generate(password);

		return await gameUserRepository.AddUser(username, hashedPassword, email);
	}
}