import Cookies from "js-cookie";
const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const getAllSessions = async ()=> {
	let token = Cookies.get('user-token');
	return await fetch(`${baseUrl}/GameAdmin/GetAllSessions/`, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		},
	});
}

export const getGameSessionAttempts = async (sessionId: string)=> {
	let token = Cookies.get('user-token');
	return await fetch(`${baseUrl}/GameAdmin/GetGameSessionAttempts/` + sessionId, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		},
	});
}

export const deleteGameSessionData = async (sessionId: string)=> {
	let token = Cookies.get('user-token');
	return await fetch(`${baseUrl}/GameAdmin/DeleteGameSessionData/` + sessionId, {
		method: 'DELETE',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		},
	});
}