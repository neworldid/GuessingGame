import {createContext, ReactNode, useContext, useState} from "react";
import { GAME_START_VIEW} from "Constants/ViewNames.ts";

interface GameProviderProps {
	children: ReactNode;
}

interface GameState {
	currentView: string;
	sessionId: string;
	playerName: string;
	errorMessage: string;
	loading: boolean;
	setCurrentView: (view: string) => void;
	setSessionId: (sessionId: string) => void;
	setPlayerName: (name: string) => void;
	setErrorMessage: (message: string) => void;
	setLoading: (loading: boolean) => void;
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

const GameStateContext = createContext<GameState | undefined>(undefined);

export const useGameContext = () => {
	const context = useContext(GameStateContext);
	if (!context) {
		throw new Error('useGameContext must be used within a GameProvider');
	}
	return context;
};