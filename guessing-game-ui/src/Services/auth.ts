import axios from "axios";
import {TokenResponse} from "@react-oauth/google";


export const getName = async (tokenResponse: TokenResponse)=> {
	try {
		let name = '';
		if (tokenResponse) {
			await axios
				.get(`https://www.googleapis.com/oauth2/v1/userinfo?access_token=${tokenResponse.access_token}`, {
					headers: {
						Authorization: `Bearer ${tokenResponse.access_token}`,
						Accept: 'application/json'
					}
				})
				.then((res) => {
					name = res.data.name;
				})
				.catch((err) => console.log(err));
		}
		return name;
	} catch (error) {
		console.error('Invalid token:', error);
		return '';
	}
}