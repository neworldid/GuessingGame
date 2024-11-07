using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameSessionService gameSessionService, IGameAttemptService gameAttemptService) : ControllerBase
{
	[HttpPost("StartGame")]
	public async Task<IActionResult> StartGame([FromBody] PlayerRequest request)
	{
		if (string.IsNullOrEmpty(request.Name))
		{
			return BadRequest("Player name is required.");
		}

		await gameSessionService.StartNewGame(request.Name);

		return Ok(new { Message = "Game started!" });
	}
	
	[HttpPost(Name = "ProcessAttempt")]
	public async Task<IActionResult> ProcessAttempt(int gameId, int attemptNumber)
	{
		var result = await gameAttemptService.ProcessAttemptAsync(gameId, attemptNumber);

		return Ok(result);
	}
}