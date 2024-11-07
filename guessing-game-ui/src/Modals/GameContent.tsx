import { useState } from 'react';
import NumericInput from "../Components/NumericInput.tsx";
import {AttemptRequest, processAttempt} from "../Services/game.ts";

export default function GameContent() {
	const [number, setNumber] = useState(1);

	const handleAttempt = async (request: AttemptRequest) => {
		try {
			const data = await processAttempt(request);
			
			//console.log("Submitted number:", number, "Response:", data);
		} catch (error) {
			console.error('There was a problem with the fetch operation:', error);
		}
	};

	return (
		<div>
			<NumericInput onChange={(e) => setNumber(Number(e.target.value))}/>
			<button onClick={() => handleAttempt({ number: number })} className="mt-4 p-2 bg-blue-500 text-white rounded">
				Submit Number
			</button>
		</div>
	)
}