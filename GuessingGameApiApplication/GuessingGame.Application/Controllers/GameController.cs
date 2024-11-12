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
	/// <summary>
	/// Starts a new game session for the player.
	/// </summary>
	/// <param name="request">The player request containing the player's name.</param>
	/// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
	/// <response code="200">The game session was successfully started.</response>
	/// <response code="400">Failed to start the game session.</response>
	[HttpPost("StartGame")]
	public async Task<IActionResult> StartGame([FromBody] PlayerRequest request)
	{
		var result = await gameSessionService.StartNewGame(request.PlayerName);

		if (result.HasValue)
			return Ok(new { GameSessionId = result.Value });

		return BadRequest("Failed to start game.");
	}
	
	/// <summary>
	/// Processes an attempt for the current game session.
	/// </summary>
	/// <param name="request">The attempt request containing the player's guess and session ID.</param>
	/// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
	/// <response code="200">The attempt was successfully processed.</response>
	/// <response code="400">Failed to process the attempt.</response>
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
	
	/// <summary>
	/// Retrieves the details of a game session by its session ID.
	/// </summary>
	/// <param name="sessionId">The unique identifier of the game session.</param>
	/// <returns>An <see cref="IActionResult"/> containing the game session details if found.</returns>
	/// <response code="200">The game session details were successfully retrieved.</response>
	/// <response code="404">The game session was not found.</response>
	[HttpGet("GetGameDetails/{sessionId:Guid}")]
	public async Task<IActionResult> GetGameDetails(Guid sessionId)
	{
		var game = await gameSessionService.GetGameDetails(sessionId);

		if (game == null)
			return NotFound();

		return Ok(game);
	}
	
	/// <summary>
	/// Retrieves the results of all game sessions.
	/// </summary>
	/// <returns>An <see cref="IActionResult"/> containing the list of game results if found.</returns>
	/// <response code="200">The game results were successfully retrieved.</response>
	/// <response code="204">No game results were found.</response>
	[HttpGet("GetGameResults")]
	public async Task<IActionResult> GetGameResults()
	{
		var results = await gameResultService.GetGameResults();
		if (results == null)
			return NoContent();

		return Ok(results);
	}
	
	/// <summary>
	/// Deletes a game result by its ID.
	/// </summary>
	/// <param name="resultId">The unique identifier of the game result to delete.</param>
	/// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
	/// <response code="200">The game result was successfully deleted.</response>
	/// <response code="404">The game result was not found or an error occurred during deletion.</response>
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