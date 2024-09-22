import React from 'react';
import useListPosts from '../../hooks/post/useListPosts';
import { Post } from '../../types/Post';
import moment from 'moment';
import { ErrorBlockProps } from '../../types/ErrorBlockProps';
import { Link } from 'react-router-dom';

const Home: React.FC = () => {
	const { posts, loading, error } = useListPosts();

	function LoadingBlock() {
		return <div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h2>Loading posts...</h2>
			</div>
		</div>
	}

	function ErrorBlock({ errorMessage }: ErrorBlockProps) {
		return <div className='w-75'>
			<div className='d-flex align-items-center mt-3 flex-column'>
				<h3>We were unable to load the posts.</h3>
				<p className='danger'>{errorMessage}</p>

				<h3>Please, refresh the page</h3>
			</div>
		</div>
	}

	if (loading) return <LoadingBlock />;
	if (error) return <ErrorBlock errorMessage={error} />;

	return (
		<div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h2>Latest posts</h2>
			</div>

			<hr />

			<div id='posts-list' className='mx-5'>
				{posts.map((post: Post) => (
					<div key={post.id} className='mb-5'>
						<h3 id='post-title'><Link to={`/post/${post.id}`}>{post.title}</Link></h3>
						<div className='d-flex justify-content-between'>
							<div className='d-flex'>
								<i className="fas fa-user"></i>
								<h6 id='post-author' className='ms-2'>{post.author.name}</h6>
							</div>

							<div className='d-flex'>
								<i className="fas fa-clock"></i>
								<h6 id='post-date' className='ms-2'>{moment(post.creationDate).format("MM/DD/YYYY HH:mm")}</h6>
							</div>
						</div>

						<div id='post-content' className='mt-3'>
							<p>{post.content.length > 400 ? `${post.content.substring(0, 400)}...` : post.content}</p>
						</div>
					</div>
				))}
			</div>
		</div>
	);
}

export default Home;
