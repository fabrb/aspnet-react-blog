import { useState } from 'react';
import { createPost } from '../../services/postService';

const useCreatePost = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const createNewPost = async (postData: any) => {
		setLoading(true);
		setError(null);
		try {
			const response = await createPost(postData);

			if (response.value.message === "Post not created") {
				setError(`Failed to create post: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to create post: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, createNewPost };
};

export default useCreatePost;
