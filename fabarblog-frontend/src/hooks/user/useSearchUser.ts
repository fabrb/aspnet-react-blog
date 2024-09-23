import { useEffect, useState } from 'react';
import axios from 'axios';
import { Post } from '../../types/Post';

const useSearchUser = (postId: string) => {
	const [post, setPost] = useState<Post>();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchUser = async () => {
			try {
				const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/user/${postId}`);
				setPost(response.data.value.details);

			} catch (err) {
				setPost(undefined)
				setError('Failed to fetch user');
			} finally {
				setLoading(false);
			}
		};

		fetchUser();
	}, []);

	return { post, loading, error };
};

export default useSearchUser;
