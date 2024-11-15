﻿export interface GameRequest {
	PlayerName: string;
}
export interface AttemptRequest {
	Number: string;
	SessionId: string;
}

export interface GameDetailRequest {
	SessionId: string;
}

const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const startGame = async (gameRequest: GameRequest)=> {
	return await fetch(`${baseUrl}/Game/StartGame/`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( gameRequest ),
	});
}

export const processAttempt = async (attemptRequest: AttemptRequest)=> {
	return await fetch(`${baseUrl}/Game/ProcessAttempt/`, {
		method: 'PUT',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( attemptRequest ),
	});
}

export const getGameDetails = async (detailRequest: GameDetailRequest)=> {
	return await fetch(`${baseUrl}/Game/GetGameDetails/` + detailRequest.SessionId, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}

export const getGameResults = async ()=> {
	return await fetch(`${baseUrl}/Game/GetGameResults/`, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}

export const deleteResult = async (resultId: number)=> {
	return await fetch(`${baseUrl}/Game/DeleteResult/` + resultId, {
		method: 'DELETE',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}