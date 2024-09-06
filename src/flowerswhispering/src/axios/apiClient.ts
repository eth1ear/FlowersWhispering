import axios from 'axios';

const apiClient = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL || 'http://www.p1nkhub.com',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;
