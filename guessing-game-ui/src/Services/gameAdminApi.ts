const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export const deleteResult = async (resultId: number)=> {
	return await fetch(`${baseUrl}/GameAdmin/DeleteResult/` + resultId, {
		method: 'DELETE',
		headers: {
			'Content-Type': 'application/json',
		},
	});
}