import {createContext, ReactNode, useContext, useEffect, useState} from "react";
import {AUTH_LOGIN_VIEW} from "Constants/ViewNames.ts";
import Cookies from "js-cookie";

interface AuthStateProvider {
	children: ReactNode;
}

interface AuthState {
	currentView: string;
	loading: boolean;
	isAdmin: boolean;
	setCurrentView: (view: string) => void;
	setLoading: (loading: boolean) => void;
	setIsAdmin: (isAdmin: boolean) => void;
}
export const AuthStateProvider = ({ children }: AuthStateProvider) => {
	const [currentView, setCurrentView] = useState(AUTH_LOGIN_VIEW);
	const [loading, setLoading] = useState(false);
	const [isAdmin, setIsAdmin] = useState(() => {
		const cookieValue = Cookies.get('isAdmin');
		return cookieValue === 'true';
	});

	useEffect(() => {
		Cookies.set('isAdmin', isAdmin.toString());
	}, [isAdmin]);
	
	return (
		<AuthStateContext.Provider
			value={{
				currentView,
				setCurrentView,
				loading,
				setLoading,
				isAdmin,
				setIsAdmin
			}}
		>
			{children}
		</AuthStateContext.Provider>
	);
};

const AuthStateContext = createContext<AuthState | undefined>(undefined);

export const useAuthContext = () => {
	const context = useContext(AuthStateContext);
	if (!context) {
		throw new Error('useGameContext must be used within a GameProvider');
	}
	return context;
};