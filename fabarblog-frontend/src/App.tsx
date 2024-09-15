import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import { Home, PostCreate, UpdatePost, UserProfile } from './pages/index';
import { Navigation } from './components/index'

const App: React.FC = () => {
	return (
		<Router>
			<div id='main'>
				<Navigation />

				<div id='body' className='container'>
					<div className='row justify-content-center'>
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/post" element={<PostCreate />} />
							<Route path="/post2" element={<UpdatePost />} />
							<Route path="/user/:userId" element={<UserProfile />} />
						</Routes>

					</div>
				</div>
			</div>
		</Router>
	);
};

export default App;
