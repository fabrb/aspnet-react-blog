import { jwtDecode } from 'jwt-decode';
import React from 'react';
import { Navigate } from 'react-router-dom';

interface ProtectedRouteProps {
	children: React.ReactNode;
	allowedRoles: string[];
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children, allowedRoles }) => {
	const token = localStorage.getItem('auth.token');

	if (!token)
		return <Navigate to="/" />;

	try {
		const decodedToken: any = jwtDecode(token);
		const userRole = decodedToken.role;

		if (allowedRoles.includes(userRole))
			return <>{children}</>;
		else
			return <Navigate to="/" />;

	} catch (error) {
		return <Navigate to="/auth" />;
	}
};

export default ProtectedRoute;
