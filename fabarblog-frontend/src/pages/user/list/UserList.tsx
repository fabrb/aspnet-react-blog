import React from 'react';
import useListUsers from '../../../hooks/user/useListUsers';
import { User } from '../../../types/User';
import { ErrorBlockProps } from '../../../types/ErrorBlockProps';
import { useNavigate } from 'react-router-dom';
import useDeleteUser from '../../../hooks/user/useDeleteUser';

const UserList: React.FC = () => {
	const { users, loading, error } = useListUsers();
	const navigate = useNavigate();

	const { loading: loadingDelete, error: errorDelete, removePost } = useDeleteUser();

	async function handleDelete(userId: string) {
		await removePost(userId);
		navigate("/");
	}

	function LoadingBlock() {
		return (
			<div className='w-75'>
				<div className='d-flex justify-content-center mt-3'>
					<h2>Loading users...</h2>
				</div>
			</div>
		);
	}

	function ErrorBlock({ errorMessage }: ErrorBlockProps) {
		return (
			<div className='w-75'>
				<div className='d-flex align-items-center mt-3 flex-column'>
					<h3>We were unable to load users.</h3>
					<p className='danger'>{errorMessage}</p>
					<h3>Please, refresh the page</h3>
				</div>
			</div>
		);
	}

	if (loading) return <LoadingBlock />;
	if (error) return <ErrorBlock errorMessage={error} />;

	return (
		<>
			<table className="table">
				<thead>
					<tr>
						<th scope="col">Username</th>
						<th scope="col">Email</th>
						<th scope="col">Role</th>
						<th scope="col">Action</th>
					</tr>
				</thead>
				<tbody>
					{users.map((user: User) => (
						<tr key={user.id}>
							<td>{user.username}</td>
							<td>{user.email}</td>
							<td>{user.role}</td>
							<td>
								<div className='d-flex'>
									<button
										type='button'
										className='d-flex btn btn-danger btn-sm'
										data-toggle="modal"
										data-target={`#confirm-delete-modal-${user.id}`}
									>
										<i className="fas fa-trash"></i>
										<small id='post-date' className='ms-2'>Delete</small>
									</button>

									{/* Modal de confirmação de exclusão para cada usuário */}
									<div className="modal fade" id={`confirm-delete-modal-${user.id}`} role="dialog">
										<div className="modal-dialog" role="document">
											<div className="modal-content">
												<div className="modal-header">
													<h5 className="modal-title">Delete confirmation</h5>
													<button type="button" className="close" data-dismiss="modal" aria-label="Close">
														<span aria-hidden="true">&times;</span>
													</button>
												</div>
												<div className="modal-body">
													<p>Are you sure you want to delete this user?</p>
													<p>This operation cannot be undone</p>
												</div>
												<div className="modal-footer">
													<button type="button" className="btn btn-secondary" data-dismiss="modal">No</button>
													<button type="button" className="btn btn-danger" onClick={() => handleDelete(user.id)} data-dismiss="modal">Yes</button>
												</div>
											</div>
										</div>
									</div>
								</div>
							</td>
						</tr>
					))}
				</tbody>
			</table>
		</>
	);
}

export default UserList;
