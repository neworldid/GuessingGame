﻿'use client'

import { Dialog, DialogBackdrop, DialogPanel } from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/solid';

interface ModalProps {
	isOpen: boolean;
	children: React.ReactNode;
	onClose: () => void;
}

export default function Modal({ isOpen, onClose, children }: ModalProps) {
	return (
		<Dialog open={isOpen} onClose={() => {}} className="relative z-10">
			<DialogBackdrop
				transition
				className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity data-[closed]:opacity-0 data-[enter]:duration-300 data-[leave]:duration-200 data-[enter]:ease-out data-[leave]:ease-in"
			/>

			<div className="fixed inset-0 z-10 w-screen overflow-y-auto">
				<div className="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
					<DialogPanel
						transition
						className="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all data-[closed]:translate-y-4 data-[closed]:opacity-0 data-[enter]:duration-300 data-[leave]:duration-200 data-[enter]:ease-out data-[leave]:ease-in sm:my-8 sm:w-full sm:max-w-lg data-[closed]:sm:translate-y-0 data-[closed]:sm:scale-95"
					>
						<button
							type="button"
							onClick={onClose}
							className="absolute top-0 right-0 m-4 text-gray-400 hover:text-gray-600"
						>
							<XMarkIcon className="h-6 w-6"/>
						</button>
							{children}
					</DialogPanel>
				</div>
			</div>
		</Dialog>
	)
}
