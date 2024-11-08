export interface GameRequest {
	PlayerName: string;
}
export interface AttemptRequest {
	number: number;
}

const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const startGame = async (gameRequest: GameRequest)=> {
	await fetch(`${baseUrl}/Game/StartGame/`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( gameRequest ),
	});
}

export const processAttempt = async (attemptRequest: AttemptRequest)=> {
	return await fetch('https://localhost:44330/Game/ProcessAttempt/', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( attemptRequest ),
	});
}