using GuessingGame.Domain.Constants;
using GuessingGame.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure.Database;

public class GuessingGameDbContext(DbContextOptions<GuessingGameDbContext> options) : DbContext(options)
{
	public DbSet<GameSession> GameSessions { get; set; }
	public DbSet<GameResult> GameResults { get; set; }
	public DbSet<GameAttempt> GameAttempts { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<GameSetting> GameSettings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<GameSession>()
			.HasOne(gs => gs.GameResult)
			.WithOne(gr => gr.GameSession)
			.HasForeignKey<GameResult>(gr => gr.GameSessionId);
		
		modelBuilder.Entity<GameSession>()
			.HasMany(gs => gs.GameAttempts)
			.WithOne(ga => ga.GameSession)
			.HasForeignKey(ga => ga.GameSessionId);

		modelBuilder.Entity<GameSetting>().HasData(
			new GameSetting
			{
				Id = (int)GameSettingsConstants.CleanUpService,
				Description = "Enable background service",
				IsEnabled = true
			}
		);

		base.OnModelCreating(modelBuilder);
	}
}