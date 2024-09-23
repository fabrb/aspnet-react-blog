import { useState } from 'react';
import { deleteUser } from '../../services/userService';

const useDeleteUser = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const removeUser = async (id: string) => {
		setLoading(true);
		setError(null);
		try {
			const response = await deleteUser(id);

			if (response.value.message === "User not deleted") {
				setError(`Failed to delete user: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to delete user: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, removeUser };
};

export default useDeleteUser;
