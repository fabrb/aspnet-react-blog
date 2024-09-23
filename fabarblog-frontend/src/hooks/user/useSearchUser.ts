import { useEffect, useState } from 'react';
import { User } from '../../types/User';
import { getUser } from '../../services/userService';

const useSearchUser = (postId: string) => {
	const [user, setUser] = useState<User | null>();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchUser = async () => {
			try {
				const response = await getUser(postId)

				if (response.value.details.message === "Not found") {
					setUser(null);
					setError('Not found');

					return
				}

				setUser(response.value.details);

			} catch (err) {
				setUser(undefined)
				setError('Failed to fetch user');
			} finally {
				setLoading(false);
			}
		};

		fetchUser();
	}, []);

	return { post: user, loading, error };
};

export default useSearchUser;
