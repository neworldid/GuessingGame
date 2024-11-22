import 'src/index.css'
import Modal from '../Modal'
import GameProcessContent from "./GameProcessContent.tsx";
import GameStartContent from "./GameStartContent.tsx";
import {GoogleOAuthProvider} from "@react-oauth/google";
import GameFinishContent from "./GameFinishContent.tsx";
import {useGameContext} from "Hooks/GameStateContext.ts";
import { GAME_START_VIEW, GAME_PROCESS_VIEW, GAME_FINISH_VIEW } from "src/Constants/ViewNames.ts";

const googleclientid = import.meta.env.VITE_REACT_APP_GOOGLE_CLIENT_ID;

interface SpecificModalProps {
	isOpen: boolean;
	onClose: () => void;
}

export default function GameModal({ isOpen, onClose }: SpecificModalProps) {
	const {sessionId, currentView, setCurrentView, setSessionId} = useGameContext();
	
	const handleClose = () => {
		onClose();
		setCurrentView(GAME_START_VIEW);
		setSessionId('');
	};
	
	return (
		<GoogleOAuthProvider clientId={googleclientid}>
			<Modal isOpen={isOpen} onClose={handleClose}>
				{currentView === GAME_START_VIEW && <GameStartContent />}
				{currentView === GAME_PROCESS_VIEW && sessionId && <GameProcessContent />}
				{currentView === GAME_FINISH_VIEW && sessionId && <GameFinishContent  />}
			</Modal>
		</GoogleOAuthProvider>
	)
}