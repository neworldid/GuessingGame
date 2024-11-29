import {useState} from 'react'
import {
	Dialog,
	DialogPanel,
	PopoverGroup,
} from '@headlessui/react'
import {
	Bars3Icon,
	XMarkIcon,
} from '@heroicons/react/24/outline'
import AuthModal from "Modals/Auth/AuthModal.tsx";
import {useAuthContext} from "Hooks/AuthStateProvider.tsx";
import {ADMIN_PAGE_VIEW, HOME_PAGE_VIEW, LEADERBOARD_PAGE_VIEW} from "Constants/ViewNames.ts";
import {GoogleOAuthProvider} from "@react-oauth/google";
import {FormStateProvider} from "Hooks/FormStateProvider.tsx";
import Cookies from "js-cookie";

export default function Header() {
	const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
	const [isModalOpen, setIsModalOpen] = useState(false);
	const {isAdmin} = useAuthContext();
	
	const handleLogout = () => {
		Cookies.set('user-token', '');
		window.location.href = HOME_PAGE_VIEW;
	}
	
	const googleclientid = import.meta.env.VITE_REACT_APP_GOOGLE_CLIENT_ID;


	return (
		<header className="bg-white">
			<GoogleOAuthProvider clientId={googleclientid}>
				<FormStateProvider>
					<AuthModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}/>
				</FormStateProvider>
			</GoogleOAuthProvider>
			<nav aria-label="Global" className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8">
				<div className="flex lg:flex-1">
					<a href={HOME_PAGE_VIEW} className="-m-1.5 p-1.5">
						<span className="sr-only">Your Company</span>
						<img alt="" src="/guessinggame.png" className="h-8 w-auto"/>
					</a>
				</div>
				<div className="flex lg:hidden">
					<button
						type="button"
						onClick={() => setMobileMenuOpen(true)}
						className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
					>
						<span className="sr-only">Open main menu</span>
						<Bars3Icon aria-hidden="true" className="h-6 w-6"/>
					</button>
				</div>
				<PopoverGroup className="hidden lg:flex lg:gap-x-12">
					<a href={HOME_PAGE_VIEW} className="text-sm font-semibold leading-6 text-gray-900">
						Game
					</a>
					<a href={LEADERBOARD_PAGE_VIEW} className="text-sm font-semibold leading-6 text-gray-900">
						Leaderboard
					</a>
					{isAdmin && (
						<a href={ADMIN_PAGE_VIEW} className="text-sm font-semibold leading-6 text-gray-900">
							Admin
						</a>
					)}
				</PopoverGroup>
				<div className="hidden lg:flex lg:flex-1 lg:justify-end hover:cursor-pointer">
					{isAdmin ? (
						<a onClick={handleLogout} className="text-sm font-semibold leading-6 text-gray-900">
							Log out
						</a>
					) : (
						<a onClick={() => setIsModalOpen(true)} className="text-sm font-semibold leading-6 text-gray-900">
							Log in <span aria-hidden="true">&rarr;</span>
						</a>
					)}
				</div>
			</nav>
			<Dialog open={mobileMenuOpen} onClose={setMobileMenuOpen} className="lg:hidden">
				<div className="fixed inset-0 z-10"/>
				<DialogPanel
					className="fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10">
					<div className="flex items-center justify-between">
						<a href="#" className="-m-1.5 p-1.5">
							<span className="sr-only">Your Company</span>
							<img
								alt=""
								src="/"
								className="h-8 w-auto"
							/>
						</a>
						<button
							type="button"
							onClick={() => setMobileMenuOpen(false)}
							className="-m-2.5 rounded-md p-2.5 text-gray-700"
						>
							<span className="sr-only">Close menu</span>
							<XMarkIcon aria-hidden="true" className="h-6 w-6"/>
						</button>
					</div>
					<div className="mt-6 flow-root">
						<div className="-my-6 divide-y divide-gray-500/10">
							<div className="space-y-2 py-6">
								<a href={HOME_PAGE_VIEW}
								   className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
									Game
								</a>
								<a href={LEADERBOARD_PAGE_VIEW}
								   className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
									Leaderboard
								</a>
								{isAdmin && (
									<a href={ADMIN_PAGE_VIEW}
									   className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
										Admin
									</a>
								)}
							</div>
							<div className="py-6">
								{isAdmin ? (
									<a href="#" onClick={handleLogout} className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
										Log out
									</a>
								) : (
									<a href="#" onClick={() => setIsModalOpen(true)} className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
										Log in
									</a>
								)}
							</div>
						</div>
					</div>
				</DialogPanel>
			</Dialog>
		</header>
	)
}
