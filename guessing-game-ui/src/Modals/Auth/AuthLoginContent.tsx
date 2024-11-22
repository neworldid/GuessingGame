import {useAuthContext} from "Hooks/AuthStateContext.ts";
import {AUTH_REGISTER_VIEW} from "Constants/ViewNames.ts";
import {login, LoginAccountData} from "Services/gameAuthApi.ts";

interface SpecificModalProps {
	onClose: () => void;
}

export default function AuthLoginContent({ onClose }: SpecificModalProps) {
	const {
		setIsAdmin,
		setCurrentView,
		email,
		setEmail,
		password,
		setPassword} = useAuthContext();
	
	const handleLogin = async () => {
		let loginData: LoginAccountData = { email: email, password: password };
		const response = await login(loginData);
		if (!response.ok){
			return
		}
		onClose();
		setIsAdmin(true);
	};
	
	return (
		<div className="flex flex-col items-center">
			<h1 className="text-2xl font-bold mb-4">Login</h1>
			<input
				type="email"
				placeholder="Email"
				value={email}
				onChange={(e) => setEmail(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-64 mb-4"
			/>
			<input
				type="password"
				placeholder="Password"
				onChange={(e) => setPassword(e.target.value)}
				className="border border-gray-300 rounded-lg p-2 w-64 mb-4"
			/>
			<button className="bg-blue-500 text-white rounded-lg p-2 w-64 hover:bg-blue-400" onClick={handleLogin}>Log in</button>
			<p className="text-sm mb-2">
				Don't have an account?
				<button className="text-blue-500 ml-1" onClick={() => setCurrentView(AUTH_REGISTER_VIEW)}>Register</button>
			</p>
		</div>
	);
}