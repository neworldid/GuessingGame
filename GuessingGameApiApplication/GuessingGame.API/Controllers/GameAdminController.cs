using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GameAdminController(IGameAttemptRepository attemptRepository, IGameSessionRepository sessionRepository) : ControllerBase
{
	/// <summary>Retrieves all game sessions.</summary>
	/// <returns>
	/// An <see cref="IActionResult"/> containing the list of all game sessions if successful; otherwise, a <see cref="BadRequestResult"/>.
	/// </returns>
	/// <response code="200">Returns the list of all game sessions.</response>
	/// <response code="400">If an error occurs while retrieving the game sessions.</response>
	[HttpGet("GetAllSessions")]
	public async Task<IActionResult> GetAllSessions()
	{
		try
		{
			var results = await sessionRepository.GetAllGameSessions();
			return Ok(results);
		}
		catch
		{
			return BadRequest();
		}
	}
	
	/// <summary>
	/// Retrieves all attempts for a specific game session.
	/// </summary>
	/// <param name="gameSessionId">The unique identifier of the game session.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> containing the list of attempts for the specified game session if successful; otherwise, a <see cref="BadRequestResult"/>.
	/// </returns>
	/// <response code="200">Returns the list of attempts for the specified game session.</response>
	/// <response code="400">If an error occurs while retrieving the attempts.</response>
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
	
	/// <summary>
	/// Deletes the data for a specific game session.
	/// </summary>
	/// <param name="gameSessionId">The unique identifier of the game session.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> indicating the result of the delete operation.
	/// </returns>
	/// <response code="200">The game session data was successfully deleted.</response>
	/// <response code="400">Failed to delete the game session data.</response>
	[HttpDelete("DeleteGameSessionData/{gameSessionId:Guid}")]
	public async Task<IActionResult> DeleteGameSessionData(Guid gameSessionId)
	{
		var result = await sessionRepository.DeleteSessionsAsync(gameSessionId);
		if (result)
			return Ok();
		
		return BadRequest();
	}
}