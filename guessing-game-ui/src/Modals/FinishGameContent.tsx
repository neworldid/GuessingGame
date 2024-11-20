import {useEffect, useState} from "react";
import {DialogTitle} from "@headlessui/react";
import { getGameDetails } from "../Services/api.ts";
import {handleStartGame} from "../Handlers/StartGameHandler.ts";
import {useGameContext} from "../Hooks/GameStateContext.ts";

export default function FinishGameContent()  {
	const [sessionDetails, setSessionDetails] = useState<any>(null);
	const {playerName, setErrorMessage, setCurrentView, sessionId, setSessionId, loading, setLoading} = useGameContext();

	useEffect(() => {
		const fetchSessionDetails = async () => {
			try {
				const response = await getGameDetails({ SessionId: sessionId });
				const data = await response.json();
				setSessionDetails(data);
			} catch (error) {
				console.error("Failed to fetch session details:", error);
			}
		};
		fetchSessionDetails();
	}, []);

	const startGame = async () => {
		setLoading(true);
		await handleStartGame({playerName, setErrorMessage, setCurrentView, setSessionId});
		setLoading(false);
	};

	return (
		<div>
			<div className="text-center sm:ml-4 sm:mt-0 sm:text-left pr-8">
				<DialogTitle as="h3" className="mt-4 text-base font-semibold leading-6 text-gray-900 min-h-10">
					{sessionDetails ? (sessionDetails.won ?
						`Congratulations, ${playerName}! You win!` :
						`Unfortunately, ${playerName}, You lose`) : ''}
				</DialogTitle>

				{sessionDetails ? (
					<div>
						<p>Secret Number: {sessionDetails.secretNumber}</p>
						<p>Attempt Count: {sessionDetails.attemptCount}</p>
						<p>Duration: {sessionDetails.duration}</p>
					</div>
				) : (
					<p>Loading session details...</p>
				)}
				
				<div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
					<button
						className={`inline-flex w-full justify-center rounded-md px-3 py-2 text-sm font-semibold text-white shadow-sm sm:ml-3 sm:w-auto ${loading ? 'bg-gray-400' : 'bg-green-600 hover:bg-green-500'}`}
						onClick={startGame} disabled={loading}>
						{loading ? 'Loading...' : 'Start Game'}
					</button>
				</div>
			</div>
		</div>
	)
}