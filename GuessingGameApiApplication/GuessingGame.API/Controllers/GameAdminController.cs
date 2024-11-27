using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameAdminController(IGameAttemptRepository attemptRepository, IGameSessionRepository sessionRepository) : ControllerBase
{
	[HttpGet("GetAllSessions")]
	public async Task<IActionResult> GetAllSessions()
	{
		try
		{
			var results = await sessionRepository.GetAllGameSessions();
			return Ok(results);
		}
		catch (Exception e)
		{
			return BadRequest();
		}
	}
	
	[HttpGet("GetGameSessionAttempts/{gameSessionId:Guid}")]
	public async Task<IActionResult> GetGameSessionAttempts(Guid gameSessionId)
	{
		try
		{
			var result = await attemptRepository.GetAttempts(gameSessionId);
			return Ok(result);
		}
		catch
		{
			return BadRequest();
		}
	}
	
	[HttpDelete("DeleteGameSessionData/{gameSessionId:Guid}")]
	public async Task<IActionResult> DeleteGameSessionData(Guid gameSessionId)
	{
		var result = await sessionRepository.DeleteSessionsAsync(gameSessionId);
		if (result)
			return Ok();
		
		return BadRequest();
	}
}