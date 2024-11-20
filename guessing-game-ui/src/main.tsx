import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import App from './Pages/App.tsx'
import './index.css'
import {BrowserRouter} from 'react-router-dom';
import {GameStateProvider} from "./Hooks/GameStateProvider.tsx";

createRoot(document.getElementById('root')!).render(
	<StrictMode>
		<BrowserRouter future={{ v7_startTransition: true, v7_relativeSplatPath: true }}>
			<GameStateProvider>
				<App/>
			</GameStateProvider>
		</BrowserRouter>
	</StrictMode>,
)
