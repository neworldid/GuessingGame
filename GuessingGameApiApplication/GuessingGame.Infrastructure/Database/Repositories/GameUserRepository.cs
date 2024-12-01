using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using GuessingGame.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Database.Repositories;

public class GameUserRepository(GuessingGameDbContext context) : IGameUserRepository
{
	public async Task<int> AddUser(string username, string password, string email)
	{
		var user = new User
		{
			Username = username,
			Password = password,
			Email = email
		};
		
		await context.Users.AddAsync(user);
		return await context.SaveChangesAsync();
	}

	public async Task<GameUserModel?> GetUserByEmail(string email)
	{
		var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
		return user == null ? null : new GameUserModel(user.Id, user.Username, user.Password, user.Email);
	}
}