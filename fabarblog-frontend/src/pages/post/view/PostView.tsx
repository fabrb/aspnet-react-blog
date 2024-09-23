import React from 'react';
import './PostView.css';
import { ErrorBlockProps } from '../../../types/ErrorBlockProps';
import useSearchPost from '../../../hooks/post/useSearchPost';
import moment from 'moment';
import { useNavigate, useParams } from 'react-router-dom';
import useDeletePost from '../../../hooks/post/useDeletePost';

const PostView: React.FC = () => {
	const { postId } = useParams<{ postId: string }>();
	const { post, loading, error } = useSearchPost(postId!);

	const { loading: loadingDelete, error: errorDelete, removePost } = useDeletePost();
	const navigate = useNavigate();

	function LoadingBlock() {
		return <div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h2>Loading post...</h2>
			</div>
		</div>
	}

	function ErrorBlock({ errorMessage }: ErrorBlockProps) {
		return <div className='w-75'>
			<div className='d-flex align-items-center mt-3 flex-column'>
				<h3>We were unable to load post.</h3>
				<p className='danger'>{errorMessage}</p>

				<h3>Please, refresh the page</h3>
			</div>
		</div>
	}

	async function handleEdit(e: React.FormEvent<HTMLButtonElement>) {
		e.preventDefault()
		navigate(`/write/${postId}`);
	}

	async function handleDelete(e: React.FormEvent<HTMLButtonElement>) {
		e.preventDefault()

		await removePost(postId!)
		navigate("/");
	}

	if (loading) return <LoadingBlock />;
	if (error) return <ErrorBlock errorMessage={error} />;

	return (
		<>
			<div className='w-75'>
				<div id='posts-list' className='mx-5 mt-5'>
					<div key={post!.id} className='mb-5'>
						<h3 id='post-title'>{post!.title}</h3>
						<div className='d-flex justify-content-between'>
							<div className='d-flex'>
								<i className="fas fa-user"></i>
								<h6 id='post-author' className='ms-2'>{post!.author.name}</h6>
							</div>

							<div className='d-flex'>
								<i className="fas fa-clock"></i>
								<h6 id='post-date' className='ms-2'>{moment(post!.creationDate).format("MM/DD/YYYY HH:mm")}</h6>
							</div>
						</div>

						<div className='d-flex justify-content-end'>
							<button type='button' className='d-flex btn btn-warning btn-sm mr-3' onClick={handleEdit}>
								<i className="fas fa-pen"></i>
								<small id='post-author' className='ms-2'>Edit</small>
							</button>

							<button type='button' className='d-flex btn btn-danger btn-sm' data-toggle="modal" data-target=".confirm-delete-modal">
								<i className="fas fa-trash"></i>
								<small id='post-date' className='ms-2'>Delete</small>
							</button>
						</div>

						<div id='post-content' className='mt-3'>
							<p>{post!.content}</p>
						</div>
					</div>
				</div>
			</div>

			{/* modal */}
			<div className="modal fade confirm-delete-modal" id="confirm-delete-modal" role="dialog">
				<div className="modal-dialog" role="document">
					<div className="modal-content">
						<div className="modal-header">
							<h5 className="modal-title">Delete confirmation</h5>
							<button type="button" className="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div className="modal-body">
							<p>
								Are you sure you want to delete this post?
							</p>

							<p>
								This operation cannot be undone
							</p>
						</div>
						<div className="modal-footer">
							<button type="button" className="btn btn-secondary" data-dismiss="modal">No</button>
							<button type="button" className="btn btn-danger" data-dismiss="modal" onClick={handleDelete}>Yes</button>
						</div>
					</div>
				</div>
			</div>
		</>
	);
}

export default PostView;
