import {useState} from "react";
import GameModal from "../Modals/GameModal.tsx";
import {GoogleOAuthProvider} from "@react-oauth/google";

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
			<GoogleOAuthProvider clientId="469941126187-528ii435blnoul134c05p7ai0ub2kt9o.apps.googleusercontent.com">
				<GameModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}/>
			</GoogleOAuthProvider>
		</>
	)
}

export default Home