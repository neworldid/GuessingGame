import {useEffect, useState} from "react";
import {DialogTitle} from "@headlessui/react";
import { getGameDetails } from "../Services/api.ts";

interface FinishGameContentProps {
	sessionId: string;
}
export default function FinishGameContent({ sessionId } : FinishGameContentProps )  {
	const [sessionDetails, setSessionDetails] = useState<any>(null);
	
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

	return (
		<div>
			<div className="text-center sm:ml-4 sm:mt-0 sm:text-left pr-8">
				<DialogTitle as="h3" className="mt-4 text-base font-semibold leading-6 text-gray-900 min-h-10">
					{sessionDetails ? (sessionDetails.won ?
						`Congratulations, ${sessionDetails.playerName}! You win!` :
						`Unfortunately, ${sessionDetails.playerName}, You lose`) : ''}
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
				
			</div>
		</div>
	)
}