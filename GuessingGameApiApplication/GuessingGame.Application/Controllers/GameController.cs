using GuessingGame.Application.Contracts;
using GuessingGame.Application.Interfaces;
using GuessingGame.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(
	IGameSessionService gameSessionService, 
	IGameAttemptService gameAttemptService,
	IGameResultService gameResultService,
	IGameResultRepository gameResultRepository) : ControllerBase
{
	[HttpPost("StartGame")]
	public async Task<IActionResult> StartGame([FromBody] PlayerRequest request)
	{
		var result = await gameSessionService.StartNewGame(request.PlayerName);

		if (result.HasValue)
			return Ok(new { GameSessionId = result.Value });

		return BadRequest("Failed to start game.");
	}
	
	[HttpPut("ProcessAttempt")]
	public async Task<IActionResult> ProcessAttempt([FromBody] AttemptRequest request)
	{
		var result = await gameAttemptService.ProcessAttemptAsync(request);
		if (result == null)
		{
			return BadRequest("Failed to process attempt.");
		}

		return Ok(result);
	}
	
	[HttpGet("GetGameDetails/{sessionId:Guid}")]
	public async Task<IActionResult> GetGameDetails(Guid sessionId)
	{
		var game = await gameSessionService.GetGameDetails(sessionId);

		if (game == null)
			return NotFound();

		return Ok(game);
	}
	
	[HttpGet("GetGameResults")]
	public async Task<IActionResult> GetGameResults()
	{
		var results = await gameResultService.GetGameResults();
		if (results == null)
			return NotFound();

		return Ok(results);
	}
	
	[HttpDelete("DeleteResult/{resultId:int}")]
	public async Task<IActionResult> DeleteResult(int resultId)
	{
		try
		{
			var result = await gameResultRepository.DeleteGameResult(resultId);
			if (result > 0)
				return Ok();
		}
		catch (Exception e)
		{
			return NotFound();
		}
		
		return NotFound();
	}
}