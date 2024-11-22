import axios from "axios";
import {TokenResponse} from "@react-oauth/google";

export interface GoogleAccountData {
	name: string;
	email: string;
	id: string;
}

export async function getGoogleAccountData(tokenResponse: TokenResponse): Promise<GoogleAccountData> {
	try {
		let data: GoogleAccountData = { name: '', email: '', id: '' };
		if (tokenResponse) {
			await axios
				.get(`https://www.googleapis.com/oauth2/v1/userinfo?access_token=${tokenResponse.access_token}`, {
					headers: {
						Authorization: `Bearer ${tokenResponse.access_token}`,
						Accept: 'application/json'
					}
				})
				.then((res) => {
					data = res.data;
				})
				.catch((err) => console.log(err));
		}
		return data;
	} catch (error) {
		console.error('Invalid token:', error);
		throw new Error('Failed to fetch Google account data');
	}
}