import {UserIcon} from "@heroicons/react/24/outline";
import {DialogTitle} from "@headlessui/react";
import {TextField} from "@mui/material";
import {useState} from "react";
import {TokenResponse, useGoogleLogin} from "@react-oauth/google";
import {getName} from "../Services/auth.ts";

export default function LoginContent() {
	const [playerName, setPlayerName] = useState('');
	const [isTouched, setIsTouched] = useState(false);

	const handleGoogleLogin = useGoogleLogin({
			onSuccess: async (tokenResponse: TokenResponse) => {
				const name = await getName(tokenResponse);
				setPlayerName(name);
			},
		});

	const handleStartGame = async () => {
		if (!playerName) {
			return;
		}
		
		try {
			debugger;
			const response = await fetch('https://localhost:44330/Game/StartGame/', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ playerName }),
			});
			if (!response.ok) {
				const errorText = await response.text();
				throw new Error(`Network response was not ok: ${errorText}`);
			}
			const data = await response.json();
			console.log("Submitted player name:", playerName, "Response:", data);
		} catch (error) {
			console.error('There was a problem with the fetch operation:', error);
		}
	};

	return (
		<>
		<div className="bg-white px-4 pb-4 pt-5 sm:p-6 sm:pb-4">
			<div className="sm:flex sm:items-start">
				<div
					className="mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-blue-100 sm:mx-0 sm:h-10 sm:w-10">
					<UserIcon aria-hidden="true" className="h-6 w-6 text-blue-500"/>
				</div>
				<div className="mt-3 text-center sm:ml-4 sm:mt-0 sm:text-left">
					<DialogTitle as="h3" className="text-base font-semibold leading-6 text-gray-900">
					</DialogTitle>
					<div className="mt-2">
						<p className="text-sm text-gray-500">

						</p>
					</div>
				</div>
			</div>
		</div>

			<TextField
				id="outlined-basic"
				label="Name"
				variant="outlined"
				value={playerName}
				onChange={(e) => {
					setPlayerName(e.target.value);
					setIsTouched(true);
				}}
				error={isTouched && !playerName}
				helperText={isTouched && !playerName ? "Name is required" : ""}
			/>

			<div className="sm:flex sm:flex-row-reverse sm:px-6">
				<button
					className="inline-flex w-full justify-center rounded-md bg-green-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500 sm:ml-3 sm:w-auto"
					onClick={handleGoogleLogin}>Sign in with Google
				</button>
				<button
					className="inline-flex w-full justify-center rounded-md bg-green-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500 sm:ml-3 sm:w-auto"
					onClick={handleStartGame}>Start Game
				</button>
			</div>
		</>
	)
}

