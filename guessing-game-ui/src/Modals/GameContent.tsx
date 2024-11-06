import { useState } from 'react';
import NumericInput from "../NumericInput.tsx";

export default function GameContent() {
	const [number, setNumber] = useState(1);

	const handleSubmit = async () => {
		try {
			const response = await fetch('/StartGame', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ number }),
			});
			if (!response.ok) {
				throw new Error('Network response was not ok');
			}
			const data = await response.json();
			console.log("Submitted number:", number, "Response:", data);
		} catch (error) {
			console.error('There was a problem with the fetch operation:', error);
		}
	};

	return (
		<div>
			<NumericInput onChange={(e) => setNumber(Number(e.target.value))}/>
			<button onClick={handleSubmit} className="mt-4 p-2 bg-blue-500 text-white rounded">
				Submit Number
			</button>
		</div>
	)
}