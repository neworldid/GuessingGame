import Modal from "Modals/Modal.tsx";
import {useEffect, useState} from "react";
import {getGameSessionAttempts} from "Services/gameAdminApi.ts";
import {format} from "date-fns";

interface SpecificModalProps {
	session: any;
	isOpen: boolean;
	onClose: () => void;
}

export default function GameSessionDetails({ session, isOpen, onClose }: SpecificModalProps) {
	const [attempts, setAttempts] = useState<any[]>([]);
	
		useEffect(() => {
			if (isOpen) {
				const fetchAttempts = async () => {
					try {
						const response = await getGameSessionAttempts(session.id);
						const data = await response.json();
						setAttempts(data);
					} catch (error) {
						console.error('Failed to fetch sessions:', error);
					}
				};
				fetchAttempts();
			}
		}, [isOpen]);
	
	
	return (
		<Modal isOpen={isOpen} onClose={onClose}>
			<div>
				<h2 className="text-lg font-bold">Session Details</h2>
				{session && (
					<div>
						<p>Player Name: {session.playerName}</p>
						<p>Start Time: {format(new Date(session.startTime), 'yyyy-MM-dd HH:mm:ss')}</p>
						<p>End Time: {session.endTime && format(new Date(session.endTime), 'yyyy-MM-dd HH:mm:ss')}</p>
					</div>
				)}
				<h3 className="text-md font-semibold mt-4">Attempts</h3>
				<ul>
					{attempts.map((attempt, index) => (
						<li key={index} className="border p-2 mb-2 rounded">
							<p>Attempt {index + 1}:</p>
							<p>Guessed Number: {attempt.guessedNumber}</p>
							<p>Attempt Number: {attempt.attemptNumber}</p>
							<p>Position Match: {attempt.positionMatch}</p>
							<p>Match In Incorrect Positions: {attempt.matchInIncorrectPositions}</p>
						</li>
					))}
				</ul>
			</div>
		</Modal>
	);
}