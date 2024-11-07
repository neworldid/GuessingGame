export interface GameRequest {
	player: string;
}
export interface AttemptRequest {
	number: number;
}

export const startGame = async (gameRequest: GameRequest)=> {
	await fetch('https://localhost:44330/Game/StartGame/', {
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