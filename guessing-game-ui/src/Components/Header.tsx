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
import GameModal from "../Modals/GameModal.tsx";
import {GoogleOAuthProvider} from "@react-oauth/google";

export default function Header() {
	const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
	const [isModalOpen, setIsModalOpen] = useState(false);

	return (
		<header className="bg-white">
			<GoogleOAuthProvider clientId="469941126187-528ii435blnoul134c05p7ai0ub2kt9o.apps.googleusercontent.com">
				<GameModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}/>
			</GoogleOAuthProvider>
			<nav aria-label="Global" className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8">
				<div className="flex lg:flex-1">
					<a href="/home" className="-m-1.5 p-1.5">
						<span className="sr-only">Your Company</span>
						<img alt="" src="" className="h-8 w-auto"/>
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
					<a href="/home" className="text-sm font-semibold leading-6 text-gray-900">
						Game rules
					</a>
					<a href="/leaderboard" className="text-sm font-semibold leading-6 text-gray-900">
						Leaderboard
					</a>
				</PopoverGroup>
				<div className="hidden lg:flex lg:flex-1 lg:justify-end hover:cursor-pointer">
					<a onClick={() => setIsModalOpen(true)} className="text-sm font-semibold leading-6 text-gray-900">
						Start Game <span aria-hidden="true">&rarr;</span>
					</a>
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
								<a href="/home"
								   className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
									Game rules
								</a>
								<a href="/leaderboard"
								   className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
									Leaderboard
								</a>
							</div>
							<div className="py-6">
								<a href="#" onClick={() => setIsModalOpen(true)}
									className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50">
									Start Game
								</a>
							</div>
						</div>
					</div>
				</DialogPanel>
			</Dialog>
		</header>
	)
}
