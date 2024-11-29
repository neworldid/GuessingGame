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
		catch (Exception e)
		{
			return BadRequest("Failed to update clean up setting.");
		}
		
		return Ok();
	}
}