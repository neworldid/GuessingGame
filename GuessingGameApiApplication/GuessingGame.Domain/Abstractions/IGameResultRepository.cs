﻿using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions;

public interface IGameResultRepository
{
	Task AddGameResult(Guid sessionId, int attemptCount, bool won);
	
	Task<IEnumerable<GameDetailsModel>> GetGameResults();

	Task<int> DeleteGameResult(int resultId);
}