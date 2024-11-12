﻿using GuessingGame.Application.Contracts;

namespace GuessingGame.Application.Interfaces;

public interface IGameSessionService
{
	Task<Guid?> StartNewGame(string playerName);

	Task<GameResultResponse?> GetGameDetails(Guid sessionId);
}