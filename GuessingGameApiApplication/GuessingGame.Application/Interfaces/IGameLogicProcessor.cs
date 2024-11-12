namespace GuessingGame.Application.Interfaces;

public interface IGameLogicProcessor
{
	string GenerateUniqueFourDigitNumber();
	
	(int positionMatch, int matchInIncorrectPositions) CalculateMatches(string secretNumber, string guessedNumber);
	
	Task<bool> GameFinished(int positionMatch, int attemptNumber, Guid sessionId);
}