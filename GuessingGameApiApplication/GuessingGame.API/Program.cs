using Autofac;
using Autofac.Extensions.DependencyInjection;
using GuessingGame.API;
using GuessingGame.API.Extensions;
using GuessingGame.Infrastructure.Authentication;
using GuessingGame.Infrastructure.BackgroundServices;
using GuessingGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	containerBuilder.RegisterModule(new GuessingGameModule());
});

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var configuration = builder.Configuration;
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddApiAuthentication(configuration);

services.AddDbContext<GuessingGameDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddHostedService<SessionCleanupService>();

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
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();