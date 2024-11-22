import {useState} from "react";
import GameModal from "Modals/Game/GameModal.tsx";
import {GameStateProvider} from "Hooks/GameStateProvider.tsx";

function Home() {
	const [isModalOpen, setIsModalOpen] = useState(false);

	return (
		<div className="p-4">
			<h1 className="text-2xl font-bold mb-4">Game Rules</h1>
			<ul className="list-disc list-inside mb-4">
				<li>Program chooses a random secret number with 4 digits. (Please note numbers started from '0' like 0417 also could be guessed)</li>
				<li>All digits in the secret number are different.</li>
				<li>Player has 8 tries to guess the secret number.</li>
				<li>
					After each guess, the program displays the message with:
					<ul className="list-disc list-inside ml-4">
						<li><strong>Position Matches</strong> is the number of digits that are correct and in the correct position.</li>
						<li><strong>Matches In Incorrect Positions</strong> is the number of digits that are correct but in the wrong position.</li>
					</ul>
				</li>
			</ul>
			
			<button
				type="button"
				className="fixed px-4 py-2 bg-blue-500 text-white rounded"
				onClick={() => setIsModalOpen(true)}>
				Start Game
			</button>
			<GameStateProvider>
				<GameModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}/>
			</GameStateProvider>
		</div>
	)
}

export default Home