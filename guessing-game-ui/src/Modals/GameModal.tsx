import '../index.css'
import Modal from './Modal'
import GameContent from "./GameContent.tsx";
import LoginContent from "./LoginContent.tsx";
import {useState} from "react";

interface SpecificModalProps {
	isOpen: boolean;
	onClose: () => void;
}

export default function GameModal({ isOpen, onClose }: SpecificModalProps) {
	const [currentView, setCurrentView] = useState('login');
	
	return (
		<Modal isOpen={isOpen} onClose={onClose}>
			<div className="mt-4">
				<button onClick={() => setCurrentView('login')} className="text-blue-500 hover:underline">Login</button>
				<button onClick={() => setCurrentView('game')} className="ml-4 text-blue-500 hover:underline">Game</button>
			</div>
			{currentView === 'login' && <LoginContent />}
			{currentView === 'game' && <GameContent />}
		</Modal>
	)
}