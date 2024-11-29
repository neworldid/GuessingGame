import Cookies from "js-cookie";

export interface SettingRequest {
	Id: number;
	IsEnabled: boolean;
}

const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const getGameSetting = async (gameSettingId: number)=> {
	let token = Cookies.get('user-token');
	return await fetch(`${baseUrl}/GameSettings/GetGameSetting/` + gameSettingId, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		},
	});
}

export const updateGameSetting = async (settingRequest: SettingRequest)=> {
	let token = Cookies.get('user-token');
	return await fetch(`${baseUrl}/GameSettings/UpdateGameSetting/`, {
		method: 'PUT',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		},
		body: JSON.stringify( settingRequest ),
	});
}