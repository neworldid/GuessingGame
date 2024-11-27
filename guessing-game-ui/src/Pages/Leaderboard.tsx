import { useEffect, useState } from "react";
import {getGameResults} from "Services/gameApi.ts";

function Leaderboard() {
	const [results, setResults] = useState<any[]>([]);

	useEffect(() => {
		const fetchResults = async () => {
			try {
				const response = await getGameResults();
				const data = await response.json();
				setResults(data);
			} catch (error) {
				console.error("Failed to fetch game results:", error);
			}
		};

		fetchResults();
	}, []);

	return (
		<div className="container mx-auto p-4">
			<h1 className="text-2xl font-bold mb-4">Leaderboard</h1>
			<div className="overflow-x-auto">
				<table className="min-w-full bg-white border border-gray-200">
					<thead>
					<tr>
						<th className="px-4 py-2 border-b">Player Name</th>
						<th className="px-4 py-2 border-b">Secret Number</th>
						<th className="px-4 py-2 border-b">Attempt Count</th>
						<th className="px-4 py-2 border-b">Duration</th>
						<th className="px-4 py-2 border-b">Result</th>
					</tr>
					</thead>
					<tbody>
					{results.map((result, index) => (
						<tr key={index} className="hover:bg-gray-100">
							<td className="px-4 py-2 border-b text-center">{result.playerName}</td>
							<td className="px-4 py-2 border-b text-center" >{result.secretNumber}</td>
							<td className="px-4 py-2 border-b text-center">{result.attemptCount}</td>
							<td className="px-4 py-2 border-b text-center">{result.duration}</td>
							<td className="px-4 py-2 border-b text-center">{result.won ? 'Won' : 'Lose'}</td>
						</tr>
					))}
					</tbody>
				</table>
			</div>
		</div>
	);
}

export default Leaderboard;