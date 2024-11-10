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
		if (string.IsNullOrEmpty(request.PlayerName))
		{
			return BadRequest("Player name is required.");
		}

		var result = await gameSessionService.StartNewGame(request.PlayerName);

		if (result.HasValue)
			return Ok(new { GameSessionId = result.Value });

		return BadRequest("Failed to start game.");
	}
	
	[HttpPost("ProcessAttempt")]
	public async Task<IActionResult> ProcessAttempt([FromBody] AttemptRequest request)
	{
		var result = await gameAttemptService.ProcessAttemptAsync(request);

		return Ok(result);
	}
}