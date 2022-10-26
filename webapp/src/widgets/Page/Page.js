import React from 'react'
import './page.css'

export default function Page({ children }) {
	return (
		<div>
			<div style={{ marginTop: '20px' }}>{children}</div>
		</div>
	)
}
