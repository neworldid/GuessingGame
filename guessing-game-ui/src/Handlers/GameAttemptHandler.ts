import {processAttempt} from "../Services/api.ts";

interface GameAttemptProps {
	number: string;
	sessionId: string;
	setPreviousGuess: (previousGuess: string | null) => void;
	setMatches: (matches: number | null) => void;
	setPositionMatches: (positionMatches: number | null) => void;
}

export const handleAttempt = async ({ number, sessionId, setPreviousGuess, setMatches, setPositionMatches }: GameAttemptProps) => {
	try {
		const response = await processAttempt({ Number: number, SessionId: sessionId });
		if (!response.ok){
			console.error('Failed to start game:', response.statusText);
			return
		}

		const data = await response.json();
		setPreviousGuess(number);
		setMatches(data.attemptDigitsMatchInNumber);
		setPositionMatches(data.positionMatches);

	} catch (error) {
		console.error('There was a problem with the fetch operation:', error);
	}
};