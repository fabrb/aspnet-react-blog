import { useState } from 'react';
import { updatePost } from '../../services/postService';

const useEditPost = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const editNewPost = async (id: string, postData: any) => {
		setLoading(true);
		setError(null);
		try {
			const response = await updatePost(id, postData);

			if (response.value.message === "Post not edited") {
				setError(`Failed to create post: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to edit post: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, editNewPost };
};

export default useEditPost;
