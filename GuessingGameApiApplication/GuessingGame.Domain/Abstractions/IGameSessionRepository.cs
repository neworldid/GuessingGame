﻿using GuessingGame.Domain.Models;

namespace GuessingGame.Domain.Abstractions;

public interface IGameSessionRepository
{
	Task<Guid?> AddGameSession(string playerName, string secretNumber);
	
	Task<GameDetailsModel> GetGameDetails(Guid sessionId);

	Task EndGame(Guid sessionId);
}