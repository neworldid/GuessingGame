import '../index.css'
import Modal from './Modal'
import GameContent from "./GameContent.tsx";
import LoginContent from "./LoginContent.tsx";
import {useState} from "react";
import {GoogleOAuthProvider} from "@react-oauth/google";
import FinishGameContent from "./FinishGameContent.tsx";

const googleclientid = import.meta.env.VITE_REACT_APP_GOOGLE_CLIENT_ID;

interface SpecificModalProps {
	isOpen: boolean;
	onClose: () => void;
}

export default function GameModal({ isOpen, onClose }: SpecificModalProps) {
	const [currentView, setCurrentView] = useState('login');
	const [sessionId, setSessionId] = useState<string>('');

	const handleClose = () => {
		onClose();
		setCurrentView('login');
		setSessionId('');
	};
	
	return (
		<GoogleOAuthProvider clientId={googleclientid}>
			<Modal isOpen={isOpen} onClose={handleClose}>
				{currentView === 'login' && <LoginContent setCurrentView={setCurrentView} setSessionId={setSessionId}/>}
				{currentView === 'game' && sessionId && <GameContent setCurrentView={setCurrentView} sessionId={sessionId} />}
				{currentView === 'finish' && sessionId && <FinishGameContent sessionId={sessionId} />}
			</Modal>
		</GoogleOAuthProvider>
	)
}