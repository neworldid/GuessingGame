import {createContext, useContext} from "react";

interface AuthState {
	currentView: string;
	errorMessage: string;
	userName: string;
	email: string;
	password: string;
	loading: boolean;
	isAdmin: boolean;
	isGoogleData: boolean;
	setCurrentView: (view: string) => void;
	setErrorMessage: (message: string) => void;
	setUserName: (name: string) => void;
	setEmail: (email: string) => void;
	setPassword: (password: string) => void;
	setLoading: (loading: boolean) => void;
	setIsAdmin: (isAdmin: boolean) => void;
	setIsGoogleData: (isGoogleData: boolean) => void;
}

export const AuthStateContext = createContext<AuthState | undefined>(undefined);

export const useAuthContext = () => {
	const context = useContext(AuthStateContext);
	if (!context) {
		throw new Error('useGameContext must be used within a GameProvider');
	}
	return context;
};