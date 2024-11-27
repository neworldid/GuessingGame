import {createContext, ReactNode, useContext, useState} from "react";

interface FormStateProvider {
	children: ReactNode;
}

interface FormState {
	userName: string;
	email: string;
	registerPassword: string;
	isGoogleData: boolean;
	setUserName: (name: string) => void;
	setEmail: (email: string) => void;
	setRegisterPassword: (password: string) => void;
	setIsGoogleData: (isGoogleData: boolean) => void;
	resetFormContext: () => void;
}

export const FormStateProvider = ({ children }: FormStateProvider) => {
	const [isGoogleData, setIsGoogleData] = useState(false);
	const [userName, setUserName] = useState('');
	const [email, setEmail] = useState('');
	const [registerPassword, setRegisterPassword] = useState('');

	const resetFormContext = () => {
		setIsGoogleData(false);
		setUserName('');
		setEmail('');
		setRegisterPassword('');
	};
	
	return (
		<FormStateContext.Provider
			value={{
				isGoogleData,
				setIsGoogleData,
				userName,
				setUserName,
				email,
				setEmail,
				registerPassword,
				setRegisterPassword,
				resetFormContext
			}}
		>
			{children}
		</FormStateContext.Provider>
	);
};

const FormStateContext = createContext<FormState | undefined>(undefined);

export const useFormContext = () => {
	const context = useContext(FormStateContext);
	if (!context) {
		throw new Error('useGameContext must be used within a GameProvider');
	}
	return context;
};