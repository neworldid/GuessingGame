using GuessingGame.API.Contracts;
using GuessingGame.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameUserController(IGameUserService gameUserService) : ControllerBase
{
	/// <summary>
	/// Authenticates a user and returns a token if successful.
	/// </summary>
	/// <param name="request">The login request containing the user's email and password.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> containing the authentication token if successful; otherwise, a <see cref="BadRequestResult"/> with an error message.
	/// </returns>
	/// <response code="200">Returns the authentication token.</response>
	/// <response code="400">If the email or password is invalid.</response>
	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
	{
		var token = await gameUserService.LoginUser(request.Email, request.Password);

		if (token is null)
		{
			return BadRequest(new {message = "Invalid email or password."});
		}
		
		return Ok(new { token});

	}
	
	/// <summary>
	/// Registers a new user with the provided username, email, and password.
	/// </summary>
	/// <param name="request">The registration request containing the user's username, email, and password.</param>
	/// <returns>
	/// An <see cref="IActionResult"/> indicating the result of the registration operation.
	/// </returns>
	/// <response code="200">The user was successfully registered.</response>
	/// <response code="400">If the user already exists or if the registration failed.</response>
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