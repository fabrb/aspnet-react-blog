import { useEffect, useState } from 'react';
import { deletePost } from '../../services/postService';

const useDeletePost = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState<string | null>(null);

	const removePost = async (id: string) => {
		setLoading(true);
		setError(null);
		try {
			const response = await deletePost(id);

			if (response.value.message === "Post not deleted") {
				setError(`Failed to delete post: ${response.value.details.message}`);
				return
			}
		} catch (err: any) {
			setError(`Failed to delete post: ${err.message}`);
		} finally {
			setLoading(false);
		}
	};

	return { loading, error, removePost };
};

export default useDeletePost;
