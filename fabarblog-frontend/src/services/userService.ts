import api from './api';

export const loginUser = async (email: string, password: string) => {
	const response = await api.post('/auth/login', { email, password });
	return response.data;
};

export const getUserProfile = async () => {
	const response = await api.get('/user/profile');
	return response.data;
};
