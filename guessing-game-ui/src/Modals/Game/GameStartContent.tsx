﻿import {UserIcon} from "@heroicons/react/24/outline";
import React from "react";
import {TokenResponse, useGoogleLogin} from "@react-oauth/google";
import {getGoogleAccountData, GoogleAccountData} from "Services/googleAuth.ts";
import {handleStartGame} from "Handlers/StartGameHandler.ts";
import NameInput from "Components/NameInput.tsx";
import {useGameContext} from "Hooks/GameStateProvider.tsx";

export default function GameStartContent() {
	const {
		setCurrentView, 
		setSessionId,
		playerName,
		setPlayerName,
		loading,
		setLoading,
		errorMessage,
		setErrorMessage} = useGameContext();
	const handleGoogleLoginClick: React.MouseEventHandler<HTMLButtonElement> = () => {
		handleGoogleLogin();
	};

	const handleGoogleLogin = useGoogleLogin({
		onSuccess: async (tokenResponse: TokenResponse) => {
			const data: GoogleAccountData = await getGoogleAccountData(tokenResponse);
			setPlayerName(data.name);
		},
	});

	const startGame = async () => {
		setLoading(true);
		await handleStartGame({playerName, setErrorMessage, setCurrentView, setSessionId});
		setLoading(false);
	};

	return (
		<>
			<div className="bg-white px-4 pb-4 pt-5 sm:p-6 sm:pb-4">
				<div className="sm:flex sm:items-start">
					<div
						className="mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-blue-100 sm:mx-0 sm:h-10 sm:w-10">
						<UserIcon aria-hidden="true" className="h-6 w-6 text-blue-500"/>
					</div>
					<button
						className="inline-flex w-full justify-center rounded-md bg-blue-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-blue-400 sm:ml-3 sm:w-auto"
						onClick={handleGoogleLoginClick}>Sign in with Google
					</button>
				</div>
			</div>

			<div className="relative my-4">
				<div className="absolute inset-0 flex items-center">
					<div className="w-full border-t border-gray-300"/>
				</div>
				<div className="relative flex justify-center text-sm">
					<span className="bg-white px-2 text-gray-500">or</span>
				</div>
			</div>

			<NameInput
				value={playerName}
				onChange={(e) => {
					setPlayerName(e.target.value);
				}}
				errorMessage={errorMessage}
			/>

			<div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
				<button
					className={`inline-flex w-full justify-center rounded-md px-3 py-2 text-sm font-semibold text-white shadow-sm sm:ml-3 sm:w-auto ${loading ? 'bg-gray-400' : 'bg-green-600 hover:bg-green-500'}`}
					onClick={startGame} disabled={loading}>
					{loading ? 'Loading...' : 'Start Game'}
				</button>
			</div>
		</>
	)
}

