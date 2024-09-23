import { useState } from 'react';
import { loginUser } from '../../services/userService';

const useAuthenticateUser = () => {
	const [auth, setAuth] = useState<string | null>(null);
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const authenticateUser = async (email: string, password: string) => {
		setLoading(true);
		setError(null);
		try {
			const response = await loginUser(email, password);

			if (response.value.message === "User not authenticated") {
				setError(`Failed to authenticate user: ${response.value.details.message}`);
				return
			}

			setAuth(response.value.details.token);
			localStorage.setItem("auth.token", response.value.details.token)

			return true
		} catch (err: any) {
			setError(`Failed to authenticate user: ${err.message}`);
			return false
		} finally {
			setLoading(false);
		}
	};

	return { auth, loading, error, authenticateUser };
};

export default useAuthenticateUser;
