using GuessingGame.Domain.Abstractions.Repositories;

namespace GuessingGame.Infrastructure.Database.Repositories;

public class GameSettingRepository(GuessingGameDbContext context) : IGameSettingRepository
{
	public async Task<bool> GetSettingValueAsync(int settingId)
	{
		var result = await context.GameSettings.FindAsync(settingId);
		return result is { IsEnabled: true };
	}

	public async Task<int> UpdateSettingValueAsync(int settingId, bool value)
	{
		var setting = await context.GameSettings.FindAsync(settingId);
		if (setting == null)
		{
			return 0;
		}

		setting.IsEnabled = value;
		return await context.SaveChangesAsync();
	}
}