const baseUrl = import.meta.env.VITE_REACT_APP_API_BASE_URL;

export interface LoginAccountData {
	email: string;
	password: string;
}

export interface RegisterAccountData {
	username: string;
	email: string;
	password: string;
}

export const login = async (accountData: LoginAccountData)=> {
	return await fetch(`${baseUrl}/GameUser/Login/`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( accountData ),
	});
}

export const register = async (accountData: RegisterAccountData)=> {
	return await fetch(`${baseUrl}/GameUser/Register/`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: JSON.stringify( accountData ),
	});
}