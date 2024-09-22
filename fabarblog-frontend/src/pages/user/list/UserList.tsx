import React from 'react';
import useListUsers from '../../../hooks/user/useListUsers';
import { User } from '../../../types/User';
import moment from 'moment';
import { ErrorBlockProps } from '../../../types/ErrorBlockProps';
import { Link } from 'react-router-dom';

const UserList: React.FC = () => {
	const { users, loading, error } = useListUsers();

	function LoadingBlock() {
		return <div className='w-75'>
			<div className='d-flex justify-content-center mt-3'>
				<h2>Loading users...</h2>
			</div>
		</div>
	}

	function ErrorBlock({ errorMessage }: ErrorBlockProps) {
		return <div className='w-75'>
			<div className='d-flex align-items-center mt-3 flex-column'>
				<h3>We were unable to load users.</h3>
				<p className='danger'>{errorMessage}</p>

				<h3>Please, refresh the page</h3>
			</div>
		</div>
	}

	if (loading) return <LoadingBlock />;
	if (error) return <ErrorBlock errorMessage={error} />;

	return (
		<div className='w-75'>
			<div id='posts-list' className='mx-5'>
				{users.map((user: User) => (
					<div key={user.id} className='mb-5'>
						<h3 id='post-title'><Link to={`/post/${user.id}`}>{user.email}</Link></h3>
						<h6 id='post-author' className='ms-2'>{user.username}</h6>
						<p>{user.role}</p>
					</div>
				))}
			</div>
		</div>
	);
}

export default UserList;
