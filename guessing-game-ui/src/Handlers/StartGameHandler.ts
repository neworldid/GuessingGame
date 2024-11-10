import {startGame} from "../Services/api.ts";

export interface StartGameProps {
	playerName: string;
	setIsTouched: (isTouched: boolean) => void;
	setCurrentView: (view: string) => void;
	setSessionId: (sessionId: string) => void;
}

export const handleStartGame = async ({ playerName, setIsTouched, setCurrentView, setSessionId }: StartGameProps) => {
	if (!playerName) {
		setIsTouched(true);
		return;
	}

	try {
		const response = await startGame({ PlayerName: playerName });
		if (!response.ok){
			console.error('Failed to start game:', response.statusText);
			return
		}

		const data = await response.json();
		setSessionId(data.gameSessionId); // Assuming the response contains a sessionId
		setCurrentView('game');

		console.log("Submitted player name:", playerName, "Response:", data);

	} catch (error) {
		console.error('There was a problem with the fetch operation:', error);
	}
};