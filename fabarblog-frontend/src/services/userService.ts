import api from './api';

export const loginUser = async (email: string, password: string) => {
	try {
		const response = await api.post('/api/auth', { email, password });
		return response.data;
	} catch (error) {
		return error
	}
};

export const createUser = async (postData: any) => {
	try {
		const response = await api.post('/api/user', postData);
		return response.data;
	} catch (error) {
		return error
	}
};

export const updateUser = async (id: string, postData: any) => {
	try {
		const response = await api.put(`/api/user/${id}`, postData);
		return response.data;
	} catch (error) {
		return error
	}
};

export const getUser = async (id: string) => {
	try {
		const response = await api.get(`/api/user/${id}`);
		return response.data;
	} catch (error) {
		return error
	}
};

export const getUsers = async () => {
	try {
		const response = await api.get(`/api/user`);
		return response.data;
	} catch (error) {
		return error
	}
};

export const deleteUser = async (id: string) => {
	try {
		const response = await api.delete(`/api/user/${id}`);
		return response.data;
	} catch (error) {
		return error
	}
};