import React, { useEffect, useState } from 'react';
import './PostWrite.css';
import { useNavigate, useParams } from 'react-router-dom';
import useSearchPost from '../../../hooks/post/useSearchPost';
import { ErrorBlockProps } from '../../../types/ErrorBlockProps';
import useCreatePost from '../../../hooks/post/useCreatePost';
import useEditPost from '../../../hooks/post/useEditPost';

const PostWrite: React.FC = () => {
	const { postId } = useParams<{ postId: string }>();
	const { post, loading, error } = useSearchPost(postId!);

	const { loading: loadingCreate, error: errorCreate, createNewPost } = useCreatePost();
	const { loading: loadingEdit, error: errorEdit, editNewPost } = useEditPost();

	const navigate = useNavigate();

	const [titleError, setTitleError] = useState("");
	const [contentError, setContentError] = useState("");

	const [formData, setFormData] = useState({
		title: '',
		content: '',
	});

	useEffect(() => {
		if (post) {
			setFormData({
				title: post.title,
				content: post.content
			});
		}
	}, [post]);

	function handleInputChange(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
		const { name, value } = event.target;
		setFormData({ ...formData, [name]: value });
	}

	function LoadingBlock() {
		return <div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h2>Loading page...</h2>
			</div>
		</div>
	}

	function ErrorBlock({ errorMessage }: ErrorBlockProps) {
		return <div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h3>We were unable to load page.</h3>
				<p className='danger'>{errorMessage}</p>

				<h3>Please, refresh the page.</h3>
			</div>
		</div>
	}

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		setTitleError("")
		setContentError("")

		if (formData.title === null || formData.title === "") {
			setTitleError("Title invalid")
			return
		}
		if (formData.content === null || formData.content === "") {
			setContentError("Content invalid")
			return
		}

		if (postId) {
			await editNewPost(postId, formData);
			if (errorEdit)
				return

		}
		else {
			await createNewPost(formData);

			if (errorCreate)
				return

		}

		navigate(`/`);
	}

	if (postId && loading) return <LoadingBlock />;
	if (postId && error) return <ErrorBlock errorMessage={error} />;

	return (
		<>
			<div className='w-75'>
				<div id='posts-list' className='m-5'>
					<form onSubmit={handleSubmit}>
						{titleError && <div className="justify-content-center d-flex text-danger mt-3">{titleError}</div>}
						<div className="form-group">
							<input
								name='title'
								type="text"
								className="form-control"
								placeholder="Title"
								value={formData.title}
								onChange={handleInputChange}
							/>
						</div>

						{contentError && <div className="justify-content-center d-flex text-danger mt-3">{contentError}</div>}
						<div className="form-group">
							<textarea
								name='content'
								className="form-control"
								placeholder="Your post content"
								value={formData.content}
								onChange={handleInputChange}
								style={{ height: "400px" }}
							/>
						</div>
						<div className='d-flex justify-content-end'>
							<button type="submit" className="d-flex btn btn-success btn-sm" disabled={loading}>
								<i className="fas fa-pen"></i>
								<small id='post-author' className='ms-2'>{loadingCreate || loadingEdit ? 'Saving...' : 'Save'}</small>
							</button>
						</div>
					</form>

					{errorCreate && <div className="justify-content-center d-flex text-danger mt-3">{errorCreate}</div>}
					{errorEdit && <div className="justify-content-center d-flex text-danger mt-3">{errorEdit}</div>}
				</div>
			</div>
		</>
	);
}

export default PostWrite;
