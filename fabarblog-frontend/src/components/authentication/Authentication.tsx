import React, { useEffect, useState } from 'react';

const Authentication: React.FC = () => {
	const [isAuthenticated, setIsAuthenticated] = useState(false);
	const [username, setUsername] = useState("");

	useEffect(() => {
		const authToken = localStorage.getItem("auth.token")
		const authUser = localStorage.getItem("auth.username")
		setUsername(authUser || "")

		if (authToken)
			setIsAuthenticated(true)
	}, []);

	function UserBlock() {
		if (isAuthenticated)
			return <><i className="fas fa-user pe-1"></i>{username}</>

		return <><i className="fas fa-user pe-1"></i>Login</>
	}

	return (
		<div className='px-4'>
			<UserBlock />
		</div >
	);
};

export default Authentication;
