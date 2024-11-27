const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const getAllSessions = async ()=> {
	return await fetch(`${baseUrl}/GameAdmin/GetAllSessions/`, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}

export const getGameSessionAttempts = async (sessionId: string)=> {
	return await fetch(`${baseUrl}/GameAdmin/GetGameSessionAttempts/` + sessionId, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}

export const deleteGameSessionData = async (sessionId: string)=> {
	return await fetch(`${baseUrl}/GameAdmin/DeleteGameSessionData/` + sessionId, {
		method: 'DELETE',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}