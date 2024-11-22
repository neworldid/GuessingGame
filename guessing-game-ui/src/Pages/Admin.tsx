import {useState} from "react";

export default function Admin() {
	const [autoCleanEnabled, setAutoCleanEnabled] = useState(false);

	const handleToggle = () => {
		setAutoCleanEnabled(!autoCleanEnabled);
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
		</div>
	);
}