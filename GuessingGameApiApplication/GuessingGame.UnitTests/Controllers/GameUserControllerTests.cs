using GuessingGame.API.Controllers;
using GuessingGame.API.Contracts;
using GuessingGame.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GuessingGame.UnitTests.Controllers
{
	public class GameUserControllerTests
	{
		private Mock<IGameUserService> _mockGameUserService;
		private GameUserController _controller;

		[SetUp]
		public void Setup()
		{
			_mockGameUserService = new Mock<IGameUserService>();
			_controller = new GameUserController(_mockGameUserService.Object);
		}

		[Test]
		public async Task Login_ReturnsOkResult_WithToken()
		{
			// Arrange
			var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
			_mockGameUserService.Setup(service => service.LoginUser(request.Email, request.Password))
				.ReturnsAsync("test_token");

			// Act
			var result = await _controller.Login(request);

			// Assert
			_mockGameUserService.Verify(service => service.LoginUser(request.Email, request.Password), Times.Once);
			var okResult = result as OkObjectResult;
			Assert.That(okResult, Is.Not.Null);
		}

		[Test]
		public async Task Login_ReturnsBadRequest_WhenTokenIsNull()
		{
			// Arrange
			var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
			_mockGameUserService.Setup(service => service.LoginUser(request.Email, request.Password))
				.ReturnsAsync((string)null);

			// Act
			var result = await _controller.Login(request);

			// Assert
			_mockGameUserService.Verify(service => service.LoginUser(request.Email, request.Password), Times.Once);
			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
		{
			// Arrange
			var request = new UserRegisterRequest
				{ Username = "testuser", Email = "test@example.com", Password = "password" };
			_mockGameUserService
				.Setup(service => service.RegisterUser(request.Username, request.Email, request.Password))
				.ReturnsAsync(1);

			// Act
			var result = await _controller.Register(request);

			// Assert
			_mockGameUserService.Verify(
				service => service.RegisterUser(request.Username, request.Email, request.Password), Times.Once);
			Assert.That(result, Is.InstanceOf<OkResult>());
		}

		[Test]
		public async Task Register_ReturnsBadRequest_WhenUserAlreadyExists()
		{
			// Arrange
			var request = new UserRegisterRequest
				{ Username = "testuser", Email = "test@example.com", Password = "password" };
			_mockGameUserService
				.Setup(service => service.RegisterUser(request.Username, request.Email, request.Password))
				.ReturnsAsync(0);

			// Act
			var result = await _controller.Register(request);

			// Assert
			_mockGameUserService.Verify(
				service => service.RegisterUser(request.Username, request.Email, request.Password), Times.Once);
			var badRequestResult = result as BadRequestObjectResult;
			Assert.That(badRequestResult, Is.Not.Null);
			Assert.That(badRequestResult.Value, Is.EqualTo("User already exists."));
		}

		[Test]
		public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
		{
			// Arrange
			var request = new UserRegisterRequest
				{ Username = "testuser", Email = "test@example.com", Password = "password" };
			_mockGameUserService
				.Setup(service => service.RegisterUser(request.Username, request.Email, request.Password))
				.ReturnsAsync(-1);

			// Act
			var result = await _controller.Register(request);

			// Assert
			_mockGameUserService.Verify(
				service => service.RegisterUser(request.Username, request.Email, request.Password), Times.Once);
			var badRequestResult = result as BadRequestObjectResult;
			Assert.That(badRequestResult, Is.Not.Null);
			Assert.That(badRequestResult.Value, Is.EqualTo("Failed to register user."));
		}
	}
}