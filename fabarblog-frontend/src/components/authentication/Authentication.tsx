import { jwtDecode } from 'jwt-decode';
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

const Authentication: React.FC = () => {
	const [isAuthenticated, setIsAuthenticated] = useState(false);
	const [username, setUsername] = useState("");

	useEffect(() => {
		const authToken = localStorage.getItem("auth.token")
		const authUser = localStorage.getItem("auth.username")
		setUsername(authUser || "")

		if (authToken) {
			setIsAuthenticated(true)

			const decodedToken: any = jwtDecode(authToken);
			setUsername(decodedToken.unique_name);
		}
	}, []);

	function UserBlock() {
		if (isAuthenticated) {
			return <>
				<div className="dropdown">
					<button className="btn btn-light dropdown-toggle btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
						<i className="fas fa-user pe-1"></i>{username}
					</button>
					<div>
						<div className="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton" >
							<a className="dropdown-item" href="#">Profile</a>
							<a className="dropdown-item" href="#">Sign out</a>
						</div>
					</div>
				</div>
			</>
		}

		return <><Link className='btn btn-sm btn-light' to="/auth"><i className="fas fa-user pe-1"></i>Login</Link></>
	}

	return (
		<div className='px-4'>
			<UserBlock />
		</div >
	);
};

export default Authentication;
