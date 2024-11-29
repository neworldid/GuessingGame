using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions.Auth;

public interface IJwtProvider
{
	string GenerateToken(GameUserModel user);
}