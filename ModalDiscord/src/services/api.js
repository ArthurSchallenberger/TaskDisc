import axios from 'axios';
import { userTokens } from '../modals/login-modal.js';
import { Agent } from 'https';


const api = axios.create({
    baseURL: 'https://localhost:7012',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000, 
    httpsAgent: new Agent({
        rejectUnauthorized: false, 
    }),
});


api.interceptors.request.use((config) => {
    const userId = config.userId; 
    const token = userId ? userTokens[userId] : null;
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export { api };