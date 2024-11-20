import {createContext, useContext} from "react";

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

export const GameStateContext = createContext<GameState | undefined>(undefined);

export const useGameContext = () => {
	const context = useContext(GameStateContext);
	if (!context) {
		throw new Error('useGameContext must be used within a GameProvider');
	}
	return context;
};