export interface SettingRequest {
	Id: number;
	IsEnabled: boolean;
}

const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const getGameSetting = async (gameSettingId: number)=> {
	return await fetch(`${baseUrl}/GameSettings/GetGameSetting/` + gameSettingId, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}

export const updateGameSetting = async (settingRequest: SettingRequest)=> {
	return await fetch(`${baseUrl}/GameSettings/UpdateGameSetting/`, {
		method: 'PUT',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( settingRequest ),
	});
}