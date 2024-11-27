using GuessingGame.Domain.Abstractions.Processors;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessingGame.Infrastructure.BackgroundServices;

public class SessionCleanupService(
	IConfiguration configuration,
	IServiceProvider serviceProvider) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var intervalInSeconds = configuration.GetValue<int>("UpdateDataIntervalInSeconds");
		var interval = TimeSpan.FromSeconds(intervalInSeconds);
		
		while (!stoppingToken.IsCancellationRequested)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var settingRepository = scope.ServiceProvider.GetRequiredService<IGameSettingRepository>();
				var sessionCleanupProcessor = scope.ServiceProvider.GetRequiredService<ISessionCleanupProcessor>();

				try
				{
					var setting = await settingRepository.GetSettingValueAsync((int)GameSettingsConstants.CleanUpService);
					if (setting)
					{
						await sessionCleanupProcessor.CleanupSessionsAsync();
					}
				}
				catch
				{
					// ignored
				}
			}
			
			await Task.Delay(interval, stoppingToken);
		}
	}
}