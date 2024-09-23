import api from './api';

export const createPost = async (postData: any) => {
	try {
		const response = await api.post('/api/post', postData);
		return response.data;
	} catch (error: any) {
		return error.response.data
	}
};

export const updatePost = async (id: string, postData: any) => {
	try {
		const response = await api.put(`/api/post/${id}`, postData);
		return response.data;
	} catch (error: any) {
		return error.response.data
	}
};

export const getPost = async (id: string) => {
	try {
		const response = await api.get(`/api/post/${id}`);
		return response.data;
	} catch (error: any) {
		return error.response.data
	}
};

export const getPosts = async () => {
	try {
		const response = await api.get(`/api/post`);
		return response.data;
	} catch (error: any) {
		return error.response.data
	}
};

export const deletePost = async (id: string) => {
	try {
		const response = await api.delete(`/api/post/${id}`);
		return response.data;
	} catch (error: any) {
		return error.response.data
	}
};