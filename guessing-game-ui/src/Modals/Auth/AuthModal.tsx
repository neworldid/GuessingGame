import 'src/index.css'
import {TokenResponse, useGoogleLogin} from "@react-oauth/google";
import Modal from "Modals/Modal.tsx";
import {useAuthContext} from "Hooks/AuthStateProvider.tsx";
import {ADMIN_PAGE_VIEW, AUTH_LOGIN_VIEW, AUTH_REGISTER_VIEW} from "Constants/ViewNames.ts";
import AuthLoginContent from "Modals/Auth/AuthLoginContent.tsx";
import AuthRegisterContent from "Modals/Auth/AuthRegisterContent.tsx";
import {UserIcon} from "@heroicons/react/24/outline";
import React from "react";
import {getGoogleAccountData} from "Services/googleAuth.ts";
import {login, LoginAccountData} from "Services/gameUserApi.ts";
import {useFormContext} from "Hooks/FormStateProvider.tsx";
import Cookies from "js-cookie";


interface SpecificModalProps {
	isOpen: boolean;
	onClose: () => void;
}

export default function AuthModal({ isOpen, onClose }: SpecificModalProps) {
	const {currentView} = useAuthContext();
	const {setUserName, setEmail, setIsGoogleData, setRegisterPassword, resetFormContext, setLoginErrorMessage} = useFormContext();

	const handleClose = () => {
		resetFormContext();
		onClose();
	};
	
	const handleGoogleLoginClick: React.MouseEventHandler<HTMLButtonElement> = () => {
		handleGoogleLogin();
	};

	const handleGoogleLogin = useGoogleLogin({
		onSuccess: async (tokenResponse: TokenResponse) => {
			const data = await getGoogleAccountData(tokenResponse);
			if (currentView === AUTH_LOGIN_VIEW) {
				let loginData: LoginAccountData = {email: data.email, password: data.id };
				const response = await login(loginData);
				if (!response.ok){
					setEmail(data.email);
					setLoginErrorMessage("Invalid email or password");
					return
				}
				handleClose();
				const responseData = await response.json();
				Cookies.set('user-token', responseData.token);
				window.location.href = ADMIN_PAGE_VIEW;
			} else {
				setUserName(data.name);
				setEmail(data.email);
				setRegisterPassword(data.id);
				setIsGoogleData(true);
			}
		},
	});

	return (
			<Modal isOpen={isOpen} onClose={handleClose}>
				<div className="bg-white px-4 pb-4 pt-5 sm:p-6 sm:pb-4">
					<div className="sm:flex sm:items-start">
						<div
							className="mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-blue-100 sm:mx-0 sm:h-10 sm:w-10">
							<UserIcon aria-hidden="true" className="h-6 w-6 text-blue-500"/>
						</div>
						<button
							className="inline-flex w-full justify-center rounded-md bg-blue-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-blue-400 sm:ml-3 sm:w-auto"
							onClick={handleGoogleLoginClick}>{currentView === AUTH_LOGIN_VIEW ? "Sign in with Google" : "Register with Google"}
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
				
				{currentView === AUTH_LOGIN_VIEW && <AuthLoginContent handleClose={handleClose}/>}
				{currentView === AUTH_REGISTER_VIEW && <AuthRegisterContent/>}
			</Modal>
	)
}