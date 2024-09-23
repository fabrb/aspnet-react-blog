import { useState } from 'react';
import { createUser } from '../../services/userService';

const useCreateUser = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const createNewUser = async (userData: any) => {
		setLoading(true);
		setError(null);
		try {
			const response = await createUser(userData);

			if (response.value.message === "User not created") {
				setError(`Failed to create user: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to create user: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, createNewUser };
};

export default useCreateUser;
