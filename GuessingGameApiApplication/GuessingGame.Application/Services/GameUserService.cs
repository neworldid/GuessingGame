using GuessingGame.Domain.Abstractions.Auth;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Abstractions.Services;

namespace GuessingGame.Application.Services;

public class GameUserService(
	IGameUserRepository gameUserRepository, 
	IPasswordHasher passwordHasher, 
	IJwtProvider jwtProvider) : IGameUserService
{
	public async Task<string?> LoginUser(string email, string password)
	{
		try
		{
			var user = await gameUserRepository.GetUserByEmail(email);
			var result = passwordHasher.Verify(password, user.Password);
			return result == false ? null : jwtProvider.GenerateToken(user);
		}
		catch
		{
			return null;
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