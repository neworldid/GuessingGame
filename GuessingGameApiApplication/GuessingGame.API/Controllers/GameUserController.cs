using GuessingGame.API.Contracts;
using GuessingGame.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameUserController(IGameUserService gameUserService) : ControllerBase
{
	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
	{
		var result = await gameUserService.LoginUser(request.Email, request.Password);

		if (!result)
		{
			return BadRequest(new {message = "Invalid email or password."});
		}
		
		return Ok();

	}
	
	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
	{
		var result = await gameUserService.RegisterUser(request.Username, request.Email, request.Password);

		return result switch
		{
			1 => Ok(),
			0 => BadRequest("User already exists."),
			_ => BadRequest("Failed to register user.")
		};
	}
}