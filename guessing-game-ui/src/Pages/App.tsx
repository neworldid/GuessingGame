import {Routes, Route} from 'react-router-dom';
import Header from "Components/Header.tsx";
import Home from "./Home.tsx";
import Leaderboard from "./Leaderboard.tsx";
import {ADMIN_PAGE_VIEW, HOME_PAGE_VIEW, LEADERBOARD_PAGE_VIEW} from "Constants/ViewNames.ts";
import {AuthStateProvider} from "Hooks/AuthStateProvider.tsx";
import Admin from "./Admin.tsx";

function App() {
	return (
		<>
			<AuthStateProvider>
				<Header />
				<div className="container mx-auto max-w-7xl p-6 lg:px-8">
					<Routes>
						<Route path="/" element={<Home/>}/>
						<Route path={HOME_PAGE_VIEW} element={<Home/>}/>
						<Route path={LEADERBOARD_PAGE_VIEW} element={<Leaderboard/>}/>
						<Route path={ADMIN_PAGE_VIEW} element={<Admin/>}/>
					</Routes>
				</div>
			</AuthStateProvider>
		</>
	)
}

export default App
