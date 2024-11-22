import {ReactNode, useEffect, useState} from "react";
import { AuthStateContext } from "./AuthStateContext";
import {AUTH_LOGIN_VIEW} from "Constants/ViewNames.ts";
import Cookies from "js-cookie";

interface AuthStateProvider {
	children: ReactNode;
}

export const AuthStateProvider = ({ children }: AuthStateProvider) => {
	const [currentView, setCurrentView] = useState(AUTH_LOGIN_VIEW);
	const [errorMessage, setErrorMessage] = useState('');
	const [loading, setLoading] = useState(false);
	const [isGoogleData, setIsGoogleData] = useState(false);
	const [userName, setUserName] = useState('');
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
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
				errorMessage,
				setErrorMessage,
				loading,
				setLoading,
				isGoogleData,
				setIsGoogleData,
				userName,
				setUserName,
				email,
				setEmail,
				password,
				setPassword,
				isAdmin,
				setIsAdmin
			}}
		>
			{children}
		</AuthStateContext.Provider>
	);
};