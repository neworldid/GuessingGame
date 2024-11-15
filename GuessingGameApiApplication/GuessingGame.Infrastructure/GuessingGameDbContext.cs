using GuessingGame.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Infrastructure;

public class GuessingGameDbContext(DbContextOptions<GuessingGameDbContext> options) : DbContext(options)
{
	public DbSet<GameSession> GameSessions { get; set; }
	public DbSet<GameResult> GameResults { get; set; }
	public DbSet<GameAttempt> GameAttempts { get; set; }

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

		base.OnModelCreating(modelBuilder);
	}
}