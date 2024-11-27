import {useAuthContext} from "Hooks/AuthStateProvider.tsx";
import {AUTH_LOGIN_VIEW} from "Constants/ViewNames.ts";
import {register, RegisterAccountData} from "Services/gameUserApi.ts";
import {useFormContext} from "Hooks/FormStateProvider.tsx";
import {useState} from "react";

export default function AuthRegisterContent() {
	const [errorMessage, setErrorMessage] = useState<string>('');
	const {setCurrentView} = useAuthContext();
	const {userName,
		setUserName,
		email,
		setEmail,
		registerPassword,
		setRegisterPassword,
		isGoogleData} = useFormContext();


	const handleRegister = async () => {
		if (!userName || !email || !registerPassword) {
			setErrorMessage("All fields are required.");
			return;
		}
		let data: RegisterAccountData = { username: userName, email: email, password: registerPassword };

		let response = await register(data);
		if (!response.ok){
			const responseData = await response.json();
			setErrorMessage(Object.values(responseData.errors).flat().join('\n'));
			return
		}
		setCurrentView(AUTH_LOGIN_VIEW);
	};

	return (
		<div className="flex flex-col items-center">
			<h1 className="text-2xl font-bold mb-4">Register</h1>
			{errorMessage && <label className="text-red-500 text-sm mb-2 w-3/4" style={{ whiteSpace: 'pre-line' }}>{errorMessage}</label>}
			<input
				type="text"
				placeholder="Username"
				value={userName}
				onChange={(e) => setUserName(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-3/4 mb-4"
			/>
			<input
				type="email"
				placeholder="Email"
				value={email}
				disabled={isGoogleData}
				onChange={(e) => setEmail(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-3/4 mb-4"
			/>
			<input
				type="password"
				placeholder={isGoogleData ? '********' : 'Password'}
				disabled={isGoogleData}
				onChange={(e) => setRegisterPassword(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-3/4 mb-4"
			/>
			<button className="bg-blue-500 text-white rounded-lg p-2 w-3/4 hover:bg-blue-400" onClick={handleRegister}>Register</button>
			<p className="text-sm mb-2">
				Already have an account?
				<button className="text-blue-500 ml-1" onClick={() => setCurrentView(AUTH_LOGIN_VIEW)}>Log in</button>
			</p>
		</div>
	);
}