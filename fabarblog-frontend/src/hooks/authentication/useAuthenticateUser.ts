import { useEffect, useState } from 'react';
import axios from 'axios';

const useAuthenticateUser = () => {
	const [posts, setPosts] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchPosts = async () => {
			try {
				const response = await axios.get(`${process.env.REACT_APP_API_URL}/posts`);
				setPosts(response.data);
			} catch (err) {
				setError('Failed to fetch posts');
			} finally {
				setLoading(false);
			}
		};

		fetchPosts();
	}, []);

	return { posts, loading, error };
};

export default useAuthenticateUser;
