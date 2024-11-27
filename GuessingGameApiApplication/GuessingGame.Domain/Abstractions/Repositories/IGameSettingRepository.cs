namespace GuessingGame.Domain.Abstractions.Repositories;

public interface IGameSettingRepository
{
	Task<bool> GetSettingValueAsync(int settingId);
	Task<int> UpdateSettingValueAsync(int settingId, bool value);
}