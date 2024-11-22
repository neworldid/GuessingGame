using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameAdminController(IGameResultRepository gameResultRepository) : ControllerBase
{
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
		catch
		{
			return NotFound();
		}
		
		return NotFound();
	}
}