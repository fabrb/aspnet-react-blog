import { useEffect, useState } from 'react';
import { User } from '../../types/User';
import { getUsers } from '../../services/userService';

const useListUsers = () => {
	const [users, setUsers] = useState<User[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchUsers = async () => {
			try {
				const response = await getUsers()

				if (response.value.details.message === "No users were created") {
					setUsers([]);
					setError('No users were created');

					return
				}

				setUsers(response.value.details);
			} catch (err) {
				setUsers([])
				setError('Failed to fetch users');
			} finally {
				setLoading(false);
			}
		};

		fetchUsers();
	}, []);

	return { users, loading, error };
};

export default useListUsers;
