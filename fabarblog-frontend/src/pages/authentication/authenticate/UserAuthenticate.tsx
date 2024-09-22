import React, { useState } from 'react';
import './UserAuthenticate.css';
import { Link, useNavigate } from 'react-router-dom';
import useAuthenticateUser from '../../../hooks/authentication/useAuthenticateUser';

const UserAuthenticate: React.FC = () => {
	const navigate = useNavigate();
	const { auth, loading, error, authenticateUser } = useAuthenticateUser();

	const [formData, setFormData] = useState({
		email: '',
		password: '',
	});

	function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
		const { name, value } = event.target;
		setFormData({ ...formData, [name]: value });
	}

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault()

		const { email, password } = formData;
		await authenticateUser(email, password)

		if (auth) {
			localStorage.setItem("auth.token", auth);
			navigate("/");
		}
	}

	return (
		<div className='w-75'>
			<div className='d-flex justify-content-center mt-5'>
				<form onSubmit={handleSubmit}>
					<div className="form-group">
						<label >Email address</label>
						<input name='email' type="email" className="form-control" aria-describedby="emailHelp" placeholder="Enter email" onChange={handleInputChange} />
					</div>
					<div className="form-group">
						<label>Password</label>
						<input name='password' type="password" className="form-control" placeholder="Password" onChange={handleInputChange} />
					</div>
					<button type="submit" className="btn btn-primary" disabled={loading}>{loading ? 'Logging in...' : 'Login'}</button>
				</form>
			</div>

			{error && <div className="justify-content-center d-flex text-danger mt-3">{error}</div>}

			<small className='d-flex justify-content-center mt-3'>Don't have an account?</small>
			<small className='d-flex justify-content-center'>
				<Link to="/">
					Create here
				</Link>
			</small>

		</div>
	);
}

export default UserAuthenticate;
