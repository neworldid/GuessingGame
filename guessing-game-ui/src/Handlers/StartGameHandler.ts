import {startGame} from "Services/gameApi.ts";
import {GAME_PROCESS_VIEW} from "Constants/ViewNames.ts";
export interface StartGameProps {
	playerName: string;
	setErrorMessage: (errorMessage: string) => void;
	setCurrentView: (view: string) => void;
	setSessionId: (sessionId: string) => void;
}

export const handleStartGame = async ({ playerName, setErrorMessage, setCurrentView, setSessionId }: StartGameProps) => {
	if (!playerName || playerName.length < 2) {
		setErrorMessage("Name is empty or too short");
		return;
	}
	setErrorMessage('');

	try {
		const response = await startGame({ PlayerName: playerName });
		const data = await response.json();

		if (!response.ok){
			const errorMessage = data.errors ? data.errors.PlayerName : 'Failed to start game';
			setErrorMessage(errorMessage);
			console.error('Failed to start game:', response.statusText);
			return
		}

		setSessionId(data.gameSessionId);
		setCurrentView(GAME_PROCESS_VIEW);

		console.log("Submitted player name:", playerName, "Response:", data);

	} catch (error) {
		console.error('There was a problem with the fetch operation:', error);
	}
};