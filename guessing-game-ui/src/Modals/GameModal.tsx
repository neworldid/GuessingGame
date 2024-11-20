import '../index.css'
import Modal from './Modal'
import GameContent from "./GameContent.tsx";
import LoginContent from "./LoginContent.tsx";
import {GoogleOAuthProvider} from "@react-oauth/google";
import FinishGameContent from "./FinishGameContent.tsx";
import {useGameContext} from "../Hooks/GameStateContext.ts";

const googleclientid = import.meta.env.VITE_REACT_APP_GOOGLE_CLIENT_ID;

interface SpecificModalProps {
	isOpen: boolean;
	onClose: () => void;
}

export default function GameModal({ isOpen, onClose }: SpecificModalProps) {
	const {sessionId, currentView, setCurrentView, setSessionId} = useGameContext();


	const handleClose = () => {
		onClose();
		setCurrentView('login');
		setSessionId('');
	};
	
	return (
		<GoogleOAuthProvider clientId={googleclientid}>
			<Modal isOpen={isOpen} onClose={handleClose}>
				{currentView === 'login' && <LoginContent />}
				{currentView === 'game' && sessionId && <GameContent />}
				{currentView === 'finish' && sessionId && <FinishGameContent  />}
			</Modal>
		</GoogleOAuthProvider>
	)
}