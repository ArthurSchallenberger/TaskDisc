import { api } from '../services/api.js';

export async function fetchUsers(userId) {
    try {
        const response = await api.get('/api/User/GetAllUsersIdAndName', { userId });
        return response.data.map(user => ({
            id: user.id,
            name: user.name,
        }));
    } catch (error) {
        console.log('\nError fetching users:', error.response?.data?.message || error.message, '\n');
        throw error;
    }
}