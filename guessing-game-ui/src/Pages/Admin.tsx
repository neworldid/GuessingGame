import {useEffect, useState} from "react";
import GameSessionsGrid from "Components/GameSessionsGrid.tsx";
import {getGameSetting, updateGameSetting} from "Services/gameSettingsApi.ts";

export default function Admin() {
	const [autoCleanEnabled, setAutoCleanEnabled] = useState(false);

	useEffect(() => {
		const fetchSetting = async () => {
			try {
				const response = await getGameSetting(1);
				const data = await response.json();
				setAutoCleanEnabled(data);
			} catch (error) {
				console.error("Failed to fetch game setting:", error);
			}
		};
		fetchSetting();
	}, []);
	
	const handleToggle = async () => {
		const newSetting = !autoCleanEnabled;
		setAutoCleanEnabled(newSetting);
		try {
			await updateGameSetting({ Id: 1, IsEnabled: newSetting });
		} catch (error) {
			console.error("Failed to update game setting:", error);
		}
	};

	return (
		<div className="admin-container">
			<div className="toggle-container">
				<label className="toggle-label">
					Enable Auto Clean of Game Sessions
					<input
						type="checkbox"
						checked={autoCleanEnabled}
						onChange={handleToggle}
						className="toggle-input"
					/>
					<span className="toggle-slider"></span>
				</label>
			</div>
			<GameSessionsGrid />
		</div>
	);
}