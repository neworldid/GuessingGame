import {useState} from "react";
import GameModal from "../Modals/GameModal.tsx";

function Home() {
	const [isModalOpen, setIsModalOpen] = useState(false);

	return (
		<>
			<button
				type="button"
				className="fixed px-4 py-2 bg-blue-500 text-white rounded"
				onClick={() => setIsModalOpen(true)}>
				Start Game
			</button>
			<GameModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}/>
		</>
	)
}

export default Home