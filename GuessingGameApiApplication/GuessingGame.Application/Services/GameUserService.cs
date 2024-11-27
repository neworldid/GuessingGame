using GuessingGame.Domain.Abstractions;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;

namespace GuessingGame.Application.Services;

public class GameUserService(IGameUserRepository gameUserRepository, IPasswordHasher passwordHasher) : IGameUserService
{
	public async Task<bool> LoginUser(string email, string password)
	{
		try
		{
			var user = await gameUserRepository.GetUserByEmail(email);
			return passwordHasher.Verify(password, user.Password);
		}
		catch
		{
			return false;
		}
	}

	public async Task<int> RegisterUser(string username, string email, string password)
	{
		try
		{
			var user = await gameUserRepository.GetUserByEmail(email);
		
			if (user != null)
			{
				return 0;
			}

			var hashedPassword = passwordHasher.Generate(password);

			return await gameUserRepository.AddUser(username, hashedPassword, email);
		}
		catch
		{
			return -1;
		}
	}
}