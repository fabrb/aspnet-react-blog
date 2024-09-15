import React from 'react';
import { useParams } from 'react-router-dom';

const UserProfile: React.FC = () => {
	const { userId } = useParams<{ userId: string }>();

	return <h1>User Profile for user {userId}</h1>;
};

export default UserProfile;
