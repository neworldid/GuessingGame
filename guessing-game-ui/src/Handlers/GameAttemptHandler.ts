import {processAttempt} from "../Services/api.ts";

interface GameAttemptProps {
	number: string;
	sessionId: string;
	setCurrentView: (view: string) => void;
	setNumber: (previousGuess: string) => void;
	setErrorMessage: (errorMessage: string) => void;
	setPreviousGuess: (previousGuess: string | null) => void;
	setAttemptNumber: (attemptNumber: number | null) => void;
	setMatches: (matches: number | null) => void;
	setPositionMatches: (positionMatches: number | null) => void;
}

export const handleAttempt = async ({ number, sessionId, setCurrentView, setNumber, setErrorMessage, setPreviousGuess, setAttemptNumber, setMatches, setPositionMatches }: GameAttemptProps) => {
	try {
		if (!number || number.length !== 4) {
			setErrorMessage("Please provide a 4 digit number");
			return;
		}
		
		setErrorMessage('');
		
		const response = await processAttempt({ Number: number, SessionId: sessionId });
		const data = await response.json();
		
		if (!response.ok){
			const errorMessage = data.errors ? data.errors.Number : 'Failed to start game';
			setErrorMessage(errorMessage);
			console.error('Failed to process attempt:', response.statusText);
			return
		}
		
		if (data.isCompleted) {
			setCurrentView('finish');
			return;
		}
		
		setNumber('');
		setPreviousGuess(number);
		setMatches(data.matchInIncorrectPositions);
		setPositionMatches(data.positionMatch);
		setAttemptNumber(data.attemptNumber);

	} catch (error) {
		console.error('There was a problem with the fetch operation:', error);
	}
};