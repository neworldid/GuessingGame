using GuessingGame.Application.Interfaces;
using GuessingGame.Application.Services;
using GuessingGame.DataAccess;
using GuessingGame.DataAccess.Repositories;
using GuessingGame.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddDbContext<GuessingGameDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddScoped<IGameSessionService, GameSessionService>();
services.AddScoped<IGameAttemptService, GameAttemptService>();
services.AddScoped<IGameResultRepository, GameResultRepository>();
services.AddScoped<IGameAttemptRepository, GameAttemptRepository>();
services.AddScoped<IGameSessionRepository, GameSessionRepository>();

// Configure CORS
services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policyBuilder =>
	{
		policyBuilder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

var app = builder.Build();
// Use CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();