import {ReactNode, useState} from "react";
import { GameStateContext } from "./GameStateContext";
import { GAME_START_VIEW} from "Constants/ViewNames.ts";

interface GameProviderProps {
	children: ReactNode;
}

export const GameStateProvider = ({ children }: GameProviderProps) => {
	const [currentView, setCurrentView] = useState(GAME_START_VIEW);
	const [sessionId, setSessionId] = useState<string>('');
	const [playerName, setPlayerName] = useState('');
	const [errorMessage, setErrorMessage] = useState('');
	const [loading, setLoading] = useState(false);

	return (
		<GameStateContext.Provider
			value={{
				currentView,
				setCurrentView,
				sessionId,
				setSessionId,
				playerName,
				setPlayerName,
				errorMessage,
				setErrorMessage,
				loading,
				setLoading,
			}}
		>
			{children}
		</GameStateContext.Provider>
	);
};