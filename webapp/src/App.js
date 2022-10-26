import React from 'react'
import { BrowserRouter as Router } from 'react-router-dom'
import NavBar from './widgets/NavBar'
import Routes from './Routes'
import './app.css'

function App() {
	return (
		<Router>
			<NavBar />
			<div className='page'>
				<Routes />
				<h2>From App Page</h2>
				<h1 className='text-3xl font-bold underline'>From App js: Working!</h1>
			</div>
		</Router>
	)
}

export default App
