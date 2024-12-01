import { useEffect, useState } from 'react';
import {deleteGameSessionData, getAllSessions} from 'Services/gameAdminApi';
import GameSessionDetails from 'Modals/Details/GameSessionDetails';
import {FaTrash} from "react-icons/fa";
import { format } from 'date-fns';

export default function GameSessionsGrid() {
	const [sessions, setSessions] = useState<any[]>([]);
	const [selectedSession, setSelectedSession] = useState<any>(null);
	const [isModalOpen, setIsModalOpen] = useState(false);
	const [loading, setLoading] = useState(false);

	useEffect(() => {
		const fetchSessions = async () => {
			try {
				const response = await getAllSessions();
				const data = await response.json();
				setSessions(data);
			} catch (error) {
				console.error('Failed to fetch sessions:', error);
			}
		};
		fetchSessions();
	}, []);

	const handleDelete = async (index: number, id: string) => {
		setLoading(true);
		try {
			await deleteGameSessionData(id);
			const newSessions = [...sessions];
			newSessions.splice(index, 1);
			setSessions(newSessions);
		} catch (error) {
			console.error("Failed to delete game result:", error);
		} finally {
			setLoading(false);
		}
	};

	const openModal = async (session: any) => {
		setSelectedSession(session);
		setIsModalOpen(true);
	};

	const closeModal = () => {
		setIsModalOpen(false);
		setSelectedSession(null);
	};

	return (
		<div>
			<table className="min-w-full divide-y divide-gray-200 border">
				<thead>
				<tr>
					<th className="px-4 py-2">Player Name</th>
					<th className="px-4 py-2">Secret Number</th>
					<th className="px-4 py-2">Start Time</th>
					<th className="px-4 py-2">End Time</th>
					<th className="px-4 py-2">Result</th>
					<th className="px-4 py-2">Actions</th>
				</tr>
				</thead>
				<tbody>
				{sessions.map((session, index) => (
					<tr key={index} onClick={() => openModal(session)} className="cursor-pointer hover:bg-gray-100">
						<td className={"px-4 py-2 border-b text-center"}>{session.playerName}</td>
						<td className={"px-4 py-2 border-b text-center"}>{session.secretNumber}</td>
						<td className={"px-4 py-2 border-b text-center"}>{format(new Date(session.startTime), 'yyyy-MM-dd HH:mm:ss')}</td>
						<td className={"px-4 py-2 border-b text-center"}>{session.endTime && format(new Date(session.endTime), 'yyyy-MM-dd HH:mm:ss')}</td>
						<td className={"px-4 py-2 border-b text-center"}>{session.won ? 'Won' : 'Lose'}</td>
						<td className="px-4 py-2 border-b text-center">
							<button onClick={(e) => {
								e.stopPropagation();
								handleDelete(index, session.sessionId)
							}}
									className="text-red-500 hover:text-red-700">
								<FaTrash/>
							</button>
						</td>
					</tr>
				))}
				</tbody>
			</table>

			<GameSessionDetails session={selectedSession} isOpen={isModalOpen} onClose={closeModal}/>
			
			{loading && (
				<div className="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center z-50">
					<div className="loader">Removing...</div>
				</div>
			)}
		</div>
	);
}