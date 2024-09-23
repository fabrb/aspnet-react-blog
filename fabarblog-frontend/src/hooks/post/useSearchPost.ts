import { useEffect, useState } from 'react';
import { Post } from '../../types/Post';
import { getPost } from '../../services/postService';

const useSearchPost = (postId: string) => {
	const [post, setPost] = useState<Post | null>();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchPost = async () => {
			try {
				const response = await getPost(postId);

				if (response.value.details.message === "Not found") {
					setPost(null);
					setError('Not found');

					return
				}

				setPost(response.value.details);

			} catch (err) {
				console.log(1, err)

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
