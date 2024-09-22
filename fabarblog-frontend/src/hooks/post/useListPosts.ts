import { useEffect, useState } from 'react';
import axios from 'axios';
import { Post } from '../../types/Post';

const useListPosts = () => {
	const [posts, setPosts] = useState<Post[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchPosts = async () => {
			try {
				const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/post`);
				setPosts(response.data.value.details);

			} catch (err) {
				setPosts([])
				setError('Failed to fetch posts');
			} finally {
				setLoading(false);
			}
		};

		fetchPosts();
	}, []);

	return { posts, loading, error };
};

export default useListPosts;
