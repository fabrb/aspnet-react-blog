import React, { useState } from 'react';
import './UserCreate.css';
import { useNavigate } from 'react-router-dom';
import { createUser } from '../../../services/userService';

const UserCreate: React.FC = () => {
	const navigate = useNavigate();

	const [usernameError, setUsernameError] = useState("");
	const [emailError, setEmailError] = useState("");
	const [passwordError, setPasswordError] = useState("");

	const [formData, setFormData] = useState({
		username: '',
		email: '',
		password: '',
	});

	function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
		const { name, value } = event.target;
		setFormData({ ...formData, [name]: value });
	}

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();

		setUsernameError("")
		setEmailError("")
		setPasswordError("")

		if (formData.username === null || formData.username === "") {
			setUsernameError("Username invalid")
			return
		}
		if (formData.email === null || formData.email === "") {
			setEmailError("Email invalid")
			return
		}
		if (formData.password === null || formData.password === "") {
			setEmailError("Password invalid")
			return
		}

		await createUser(formData);

		navigate(`/`);
	}

	return (
		<>
			<div className='w-75'>
				<div id='posts-list' className='m-5'>
					<form onSubmit={handleSubmit}>
						{usernameError && <div className="justify-content-center d-flex text-danger mt-3">{usernameError}</div>}
						<div className="form-group">
							<input
								name='username'
								type="text"
								className="form-control"
								placeholder="Your username"
								value={formData.username}
								onChange={handleInputChange}
							/>
						</div>

						{emailError && <div className="justify-content-center d-flex text-danger mt-3">{emailError}</div>}
						<div className="form-group">
							<input
								name='email'
								className="form-control"
								placeholder="Your email"
								value={formData.email}
								onChange={handleInputChange}
							/>
						</div>

						{passwordError && <div className="justify-content-center d-flex text-danger mt-3">{passwordError}</div>}
						<div className="form-group">
							<input
								type='password'
								name='password'
								className="form-control"
								placeholder="******"
								value={formData.password}
								onChange={handleInputChange}
							/>
						</div>
						<div className='d-flex justify-content-end'>
							<button type="submit" className="d-flex btn btn-success btn-sm">
								<i className="fas fa-pen"></i>
								<small id='post-author' className='ms-2'>Save</small>
							</button>
						</div>
					</form>
				</div>
			</div>
		</>
	);
}

export default UserCreate;
