import { useEffect, useState } from 'react';
import axios from 'axios';
import { Post } from '../../types/Post';

const useSearchPost = (postId: string) => {
	const [post, setPost] = useState<Post>();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchPost = async () => {
			try {
				const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/post/${postId}`);
				setPost(response.data.value.details);

			} catch (err) {
				setPost(undefined)
				setError('Failed to fetch post');
			} finally {
				setLoading(false);
			}
		};

		fetchPost();
	}, []);

	return { post, loading, error };
};

export default useSearchPost;
