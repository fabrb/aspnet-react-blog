import api from './api';

export const createPost = async (postData: any) => {
	const response = await api.post('/post', postData);
	return response.data;
};

export const updatePost = async (id: string, postData: any) => {
	const response = await api.put(`/post/${id}`, postData);
	return response.data;
};
