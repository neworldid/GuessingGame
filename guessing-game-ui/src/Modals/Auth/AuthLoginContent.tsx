import {useAuthContext} from "Hooks/AuthStateProvider.tsx";
import {ADMIN_PAGE_VIEW, AUTH_REGISTER_VIEW} from "Constants/ViewNames.ts";
import {login, LoginAccountData} from "Services/gameUserApi.ts";
import {useState} from "react";
import {useFormContext} from "Hooks/FormStateProvider.tsx";
import Cookies from "js-cookie";

interface SpecificModalProps {
	handleClose: () => void;
}

export default function AuthLoginContent({ handleClose }: SpecificModalProps) {
	const {setCurrentView} = useAuthContext();
	const {email, setEmail, loginErrorMessage, setLoginErrorMessage} = useFormContext();
	const [password, setPassword] = useState('');


	const handleLogin = async () => {
		let loginData: LoginAccountData = { email: email, password: password };
		if (!email || !password) {
			setLoginErrorMessage("All fields are required.");
			return;
		}
		const response = await login(loginData);
		const responseData = await response.json();
		if (!response.ok){
			setLoginErrorMessage(responseData.message);
			return
		}
		handleClose();
		Cookies.set('user-token', responseData.token);
		window.location.href = ADMIN_PAGE_VIEW;

	};
	
	return (
		<div className="flex flex-col items-center">
			<h1 className="text-2xl font-bold mb-4">Login</h1>
			{loginErrorMessage && <label className="text-red-500 text-sm mb-2 w-3/4" style={{ whiteSpace: 'pre-line' }}>{loginErrorMessage}</label>}
			<input
				type="email"
				placeholder="Email"
				value={email}
				onChange={(e) => setEmail(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-3/4 mb-4"
			/>
			<input
				type="password"
				placeholder="Password"
				value={password}
				onChange={(e) => setPassword(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-3/4 mb-4"
			/>
			<button className="bg-blue-500 text-white rounded-lg p-2 w-3/4 hover:bg-blue-400" onClick={handleLogin}>Log in</button>
			<p className="text-sm mb-2">
				Don't have an account?
				<button className="text-blue-500 ml-1" onClick={() => setCurrentView(AUTH_REGISTER_VIEW)}>Register</button>
			</p>
		</div>
	);
}