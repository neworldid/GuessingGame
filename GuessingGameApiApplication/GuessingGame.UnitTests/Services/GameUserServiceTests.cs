using GuessingGame.Application.Services;
using GuessingGame.Domain.Abstractions.Auth;
using GuessingGame.Domain.Abstractions.Repositories;
using GuessingGame.Domain.Models;
using Moq;

namespace GuessingGame.UnitTests.Services
{
    public class GameUserServiceTests
    {
        private Mock<IGameUserRepository> _mockUserRepository;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IJwtProvider> _mockJwtProvider;
        private GameUserService _gameUserService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IGameUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockJwtProvider = new Mock<IJwtProvider>();
            _gameUserService = new GameUserService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockJwtProvider.Object);
        }

        [Test]
        public async Task LoginUser_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new GameUserModel(1, "testUserName", "password", "testuser@example.com");
            _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ReturnsAsync(user);
            _mockPasswordHasher.Setup(hasher => hasher.Verify("password", "password")).Returns(true);
            _mockJwtProvider.Setup(provider => provider.GenerateToken(user)).Returns("token");

            // Act
            var result = await _gameUserService.LoginUser("testuser@example.com", "password");

            // Assert
            _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.Verify("password", "password"), Times.Once);
            _mockJwtProvider.Verify(p => p.GenerateToken(user), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("token"));
        }

        [Test]
        public async Task LoginUser_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var user = new GameUserModel(1, "testUserName", "wrongpassword", "testuser@example.com");
            _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ReturnsAsync(user);
            _mockPasswordHasher.Setup(hasher => hasher.Verify("wrongpassword", user.Password)).Returns(false);

            // Act
            var result = await _gameUserService.LoginUser("testuser@example.com", "wrongpassword");

            // Assert
            _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.Verify("wrongpassword", user.Password), Times.Once);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoginUser_ReturnsNull_WhenUserRepositoryThrowsException()
		{
	        // Arrange
	        var user = new GameUserModel(1, "testUserName", "wrongpassword", "testuser@example.com");
	        _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ThrowsAsync(new Exception());

	        // Act
	        var result = await _gameUserService.LoginUser("testuser@example.com", "wrongpassword");

	        // Assert
            
	        _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
	        Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RegisterUser_ShouldReturnZero_WhenUserAlreadyExists()
        {
            // Arrange
            var user = new GameUserModel(1, "testuser", "password", "testuser@example.com");
            _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ReturnsAsync(user);

            // Act
            var result = await _gameUserService.RegisterUser("testuser", "testuser@example.com", "password");

            // Assert
            _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task RegisterUser_ShouldReturnOne_WhenUserIsSuccessfullyRegistered()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ReturnsAsync((GameUserModel)null);
            _mockPasswordHasher.Setup(hasher => hasher.Generate("password")).Returns("hashedpassword");
            _mockUserRepository.Setup(repo => repo.AddUser("testuser", "hashedpassword", "testuser@example.com")).ReturnsAsync(1);

            // Act
            var result = await _gameUserService.RegisterUser("testuser", "testuser@example.com", "password");

            // Assert
            _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.Generate("password"), Times.Once);
            _mockUserRepository.Verify(repo => repo.AddUser("testuser", "hashedpassword", "testuser@example.com"), Times.Once);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task RegisterUser_ShouldReturnMinusOne_WhenRegistrationFails()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUserByEmail("testuser@example.com")).ThrowsAsync(new Exception());

            // Act
            var result = await _gameUserService.RegisterUser("testuser", "testuser@example.com", "password");

            // Assert
            _mockUserRepository.Verify(repo => repo.GetUserByEmail("testuser@example.com"), Times.Once);
            Assert.That(result, Is.EqualTo(-1));
        }
    }
}