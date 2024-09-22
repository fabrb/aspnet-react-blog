import React from 'react';
import { Link } from 'react-router-dom';
import { Authentication } from '../index'

const Navigation: React.FC = () => {
	return (
		<div id='navigation' className='d-flex justify-content-between bg-dark text-bg-primary m-0 py-2'>
			<ul className='d-flex flex-row m-0 px-4'>
				<li className='px-1'><Link className='btn btn-sm btn-light' to="/">Home</Link></li>
				<li className='px-1'><Link className='btn btn-sm btn-light' to="/write">Write New Post</Link></li>
			</ul>

			<Authentication />
		</div>
	);
};

export default Navigation;
