import { useEffect, useState } from "react";
import {getGameResults} from "Services/gameApi.ts";
import {deleteResult} from "Services/gameAdminApi.ts";
import { FaTrash } from "react-icons/fa";

function Leaderboard() {
	const [results, setResults] = useState<any[]>([]);
	const [loading, setLoading] = useState(false);

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

	const handleDelete = async (index: number, id: number) => {
		setLoading(true);
		try {
			await deleteResult(id);
			const newResults = [...results];
			newResults.splice(index, 1);
			setResults(newResults);
		} catch (error) {
			console.error("Failed to delete game result:", error);
		} finally {
			setLoading(false);
		}
	};

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
						<th className="px-4 py-2 border-b">Actions</th>
					</tr>
					</thead>
					<tbody>
					{results.map((result, index) => (
						<tr key={index} className="hover:bg-gray-100">
							<td className="px-4 py-2 border-b">{result.playerName}</td>
							<td className="px-4 py-2 border-b">{result.secretNumber}</td>
							<td className="px-4 py-2 border-b">{result.attemptCount}</td>
							<td className="px-4 py-2 border-b">{result.duration}</td>
							<td className="px-4 py-2 border-b">{result.won ? 'Won' : 'Lose'}</td>
							<td className="px-4 py-2 border-b text-center">
								<button onClick={() => handleDelete(index, result.id)}
										className="text-red-500 hover:text-red-700">
									<FaTrash/>
								</button>
							</td>
						</tr>
					))}
					</tbody>
				</table>
			</div>
			{loading && (
				<div className="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center z-50">
					<div className="loader">Removing...</div>
				</div>
			)}
		</div>
	);
}

export default Leaderboard;