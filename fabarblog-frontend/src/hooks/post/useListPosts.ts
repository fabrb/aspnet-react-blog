import { useEffect, useState } from 'react';
import { Post } from '../../types/Post';
import { getPosts } from '../../services/postService';

const useListPosts = () => {
	const [posts, setPosts] = useState<Post[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchPosts = async () => {
			try {
				const response = await getPosts();

				if (response.value.details.message === "No posts were created") {
					setPosts([]);
					setError('No posts were created');

					return
				}

				setPosts(response.value.details);

			} catch (err: any) {
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
