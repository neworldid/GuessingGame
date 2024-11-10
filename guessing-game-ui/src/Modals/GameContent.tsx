import { useState } from 'react';
import NumericInput from "../Components/NumericInput.tsx";
import {handleAttempt} from "../Handlers/GameAttemptHandler.ts";

interface GameContentProps {
	sessionId: string;
}
export default function GameContent({ sessionId }: GameContentProps) {
	const [number, setNumber] = useState("");
	const [previousGuess, setPreviousGuess] = useState<string | null>(null);
	const [matches, setMatches] = useState<number | null>(null);
	const [positionMatches, setPositionMatches] = useState<number | null>(null);

	return (
		<div>
			{previousGuess !== null && positionMatches !== null && (
				<div>
					<label>Previous Guess: {previousGuess}, Matches: {matches}, Position Matches: {positionMatches}</label>
				</div>
			)}
			<NumericInput onChange={(e) => setNumber(e.target.value)}/>
			<button onClick={() => handleAttempt({number, sessionId, setPreviousGuess, setMatches, setPositionMatches})} className="mt-4 p-2 bg-blue-500 text-white rounded">
				Submit Number
			</button>
		</div>
	)
}