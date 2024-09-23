import { jwtDecode } from 'jwt-decode';
import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

const Authentication: React.FC = () => {
	const [isAuthenticated, setIsAuthenticated] = useState(false);
	const [username, setUsername] = useState("");

	const navigate = useNavigate();

	const checkAuthToken = () => {
		const authToken = localStorage.getItem("auth.token");

		if (authToken) {
			setIsAuthenticated(true);
			const decodedToken: any = jwtDecode(authToken);
			setUsername(decodedToken.unique_name);
		} else {
			setIsAuthenticated(false);
			setUsername("");
		}
	};

	useEffect(() => {
		checkAuthToken();
		window.addEventListener("storage", checkAuthToken);
		return () => {
			window.removeEventListener("storage", checkAuthToken);
		};
	}, []);

	function signOut() {
		localStorage.removeItem("auth.token")
		checkAuthToken();
		navigate("/");
		navigate(0);
	}

	function UserBlock() {
		if (isAuthenticated) {
			return <>
				<div className="dropdown">
					<button className="btn btn-light dropdown-toggle btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
						<i className="fas fa-user pe-1"></i>{username}
					</button>
					<div>
						<div className="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton" >
							<a className="dropdown-item" onClick={signOut}>Sign out</a>
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
