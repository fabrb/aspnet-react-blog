import axios from 'axios';

const api = axios.create({
	baseURL: process.env.REACT_APP_API_URL || 'http://localhost:5000',
	headers: {
		'Content-Type': 'application/json',
	},
});

api.interceptors.request.use((config) => {
	const token = localStorage.getItem('auth.token');
	if (token) {
		config.headers.Authorization = `Bearer ${token}`;
	}
	return config;
});

export default api;
