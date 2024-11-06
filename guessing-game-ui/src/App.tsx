import './App.css'
import {Routes, Route} from 'react-router-dom';
import Header from "./Header.tsx";
import Home from "./Home.tsx";
import Leaderboard from "./Leaderboard.tsx";

function App() {

	return (
		<>
		<Header />
		<div className="container mx-auto max-w-7xl p-6 lg:px-8">

			<Routes>
				<Route path="/" element={<Home/>}/>
				<Route path="/home" element={<Home/>}/>
				<Route path="/leaderboard" element={<Leaderboard/>}/>
		</Routes>
		</div>

		</>
)
}

export default App
