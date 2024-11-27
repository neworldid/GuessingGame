namespace GuessingGame.Domain.Abstractions.Processors;

public interface IGameLogicProcessor
{
	(int positionMatch, int matchInIncorrectPositions) CalculateMatches(string secretNumber, string guessedNumber);
	
	Task<bool> GameFinished(int positionMatch, int attemptNumber, Guid sessionId);
}