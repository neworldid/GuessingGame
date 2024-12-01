using GuessingGame.API.Contracts;
using GuessingGame.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GameSettingsController(IGameSettingRepository settingRepository) : ControllerBase
{
	/// <summary>
	/// Retrieves the value of a specific game setting.
	/// </summary>
	/// <param name="gameSettingId">The unique identifier of the game setting.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> containing the value of the specified game setting if found; otherwise, a <see cref="NotFoundResult"/>.
	/// </returns>
	/// <response code="200">Returns the value of the specified game setting.</response>
	/// <response code="404">If the game setting is not found.</response>
	[HttpGet("GetGameSetting/{gameSettingId:int}")]
	public async Task<IActionResult> GetGameSetting(int gameSettingId)
	{
		bool settingValue;
		try
		{
			settingValue = await settingRepository.GetSettingValueAsync(gameSettingId);
		}
		catch
		{
			return NotFound();
		}
		
		return Ok(settingValue);
	}
	
	/// <summary>
	/// Updates the value of a specific game setting.
	/// </summary>
	/// <param name="request">The request containing the game setting ID and the new value.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> indicating the result of the update operation.
	/// </returns>
	/// <response code="200">The game setting was successfully updated.</response>
	/// <response code="400">Failed to update the game setting.</response>
	/// <response code="404">The game setting was not found.</response>
	[HttpPut("UpdateGameSetting")]
	public async Task<IActionResult> UpdateGameSetting([FromBody] GameSettingRequest request)
	{
		try
		{
			var result = await settingRepository.UpdateSettingValueAsync(request.Id, request.IsEnabled);
			if (result == 0)
			{
				return NotFound();
			}
		}
		catch
		{
			return BadRequest("Failed to update clean up setting.");
		}
		
		return Ok();
	}
}