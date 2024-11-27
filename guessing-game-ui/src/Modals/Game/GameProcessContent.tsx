import { useState } from 'react';
import GuessedNumberInput from "Components/GuessedNumberInput.tsx";
import {handleAttempt} from "Handlers/GameAttemptHandler.ts";
import {DialogTitle} from "@headlessui/react";
import {useGameContext} from "Hooks/GameStateProvider.tsx";

export default function GameProcessContent() {
	const [number, setNumber] = useState("");
	const [previousGuess, setPreviousGuess] = useState<string | null>(null);
	const [triesLeft, setTriesLeft] = useState<number | null>(null);
	const [matches, setMatches] = useState<number | null>(null);
	const [positionMatches, setPositionMatches] = useState<number | null>(null);

	const {
		sessionId,
		setCurrentView,
		loading,
		setLoading,
		errorMessage,
		setErrorMessage} = useGameContext();
	
	const attemptGuess = async () => {
		setLoading(true);
		await handleAttempt({
			number,
			sessionId,
			setCurrentView,
			setNumber,
			setErrorMessage,
			setPreviousGuess,
			setTriesLeft,
			setMatches,
			setPositionMatches
		});
		setLoading(false);
	};

	return (
		<div>
			<div className="text-center sm:ml-4 sm:mt-0 sm:text-left pr-8">
				<DialogTitle as="h3" className="mt-4 text-base font-semibold leading-6 text-gray-900 min-h-10">
					{previousGuess !== null && (
						<div>
							<div>Previous Guess Number: {previousGuess}</div>
							<div>Tries Left: {triesLeft}</div>
							<div>Position Matches: {positionMatches}</div>
							<div>Matches In Incorrect Positions: {matches}</div>
						</div>
					) || "Secret Number was generated. Please note numbers started from '0' like 0417 also could be guessed"}
				</DialogTitle>
			</div>

			<GuessedNumberInput
				value={number}
				onChange={(e) => setNumber(e.target.value)}
				errorMessage={errorMessage}/>
			<div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
				<button onClick={attemptGuess}
						className={`mt-4 p-2 text-white rounded ${loading ? 'bg-gray-400' : 'bg-green-600 hover:bg-green-500'}`}
						disabled={loading}>
					{loading ? 'Loading...' : 'Make Guess'}
				</button>
			</div>
		</div>
	)
}