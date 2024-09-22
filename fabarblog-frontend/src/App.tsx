import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';

import { Home, PostView, UserProfile, PostWrite, UserAuthenticate, UserList } from './pages/index';
import { Navigation } from './components/index'
import ProtectedRoute from './components/route/ProtectedRoute';

const App: React.FC = () => {
	return (
		<Router>
			<div id='main'>
				<Navigation />

				<div id='body' className='container'>
					<div className='row justify-content-center'>
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/post/:postId" element={<PostView />} />

							<Route path="/write" element={<PostWrite />} />
							<Route path="/write/:postId" element={<PostWrite />} />

							<Route path="/auth" element={<UserAuthenticate />} />

							<Route path="/users" element={
								<ProtectedRoute allowedRoles={['ADMIN']}>
									<UserList />
								</ProtectedRoute>
							} />
							<Route path="/user/:userId" element={<UserProfile />} />

							<Route
								path="*"
								element={<Navigate to="/" replace />}
							/>
						</Routes>

					</div>
				</div>
			</div>
		</Router>
	);
};

export default App;
