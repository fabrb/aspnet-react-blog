import { useState } from 'react';
import { updateUser } from '../../services/userService';

const useEditUser = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const editUser = async (id: string, userData: any) => {
		setLoading(true);
		setError(null);
		try {
			const response = await updateUser(id, userData);

			if (response.value.message === "User not edited") {
				setError(`Failed to edit user: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to edit user: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, editUser };
};

export default useEditUser;
