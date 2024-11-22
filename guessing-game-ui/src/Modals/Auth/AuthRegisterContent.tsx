import {useAuthContext} from "Hooks/AuthStateContext.ts";
import {AUTH_LOGIN_VIEW} from "Constants/ViewNames.ts";
import {register, RegisterAccountData} from "Services/gameAuthApi.ts";

export default function AuthRegisterContent() {
	const { 
		setCurrentView, 
		userName, 
		setUserName, 
		email, 
		setEmail, 
		password, 
		setPassword,
		isGoogleData} = useAuthContext();

	const handleRegister = async () => {
		let data: RegisterAccountData = { username: userName, email: email, password: password };
		
		await register(data);
	};

	return (
		<div className="flex flex-col items-center">
			<h1 className="text-2xl font-bold mb-4">Register</h1>
			<input
				type="text"
				placeholder="Username"
				value={userName}
				onChange={(e) => setUserName(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-64 mb-4"
			/>
			<input
				type="email"
				placeholder="Email"
				value={email}
				disabled={isGoogleData}
				onChange={(e) => setEmail(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-64 mb-4"
			/>
			<input
				type="password"
				placeholder={isGoogleData ? '********' : 'Password'}
				disabled={isGoogleData}
				onChange={(e) => setPassword(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-64 mb-4"
			/>
			<button className="bg-blue-500 text-white rounded-lg p-2 w-64 hover:bg-blue-400" onClick={handleRegister}>Register</button>
			<p className="text-sm mb-2">
				Already have an account?
				<button className="text-blue-500 ml-1" onClick={() => setCurrentView(AUTH_LOGIN_VIEW)}>Log in</button>
			</p>
		</div>
	);
}