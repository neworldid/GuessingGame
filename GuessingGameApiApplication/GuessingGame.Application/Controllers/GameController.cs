using GuessingGame.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
	[HttpPost("StartGame")]
	public async Task<IActionResult> StartGame([FromBody] PlayerRequest request)
	{
		if (string.IsNullOrEmpty(request.Name))
		{
			return BadRequest("Player name is required.");
		}

		/*var secretNumber = new Random().Next(1, 10000);
		var gameSession = new GameSession
		{
			PlayerName = playerName,
			SecretNumber = secretNumber,
			StartTime = DateTime.UtcNow
		};

		context.GameSessions.Add(gameSession);
		await context.SaveChangesAsync();

		logger.LogInformation("Game started for {PlayerName} with secret number {SecretNumber}", playerName, secretNumber);*/

		return Ok(new { GameSessionId = 1, Message = "Game started!" });
	}
	
	[HttpPost(Name = "ProcessAttempt")]
	public IActionResult ProcessAttempt()
	{
		return Ok();
	}
}