import axios from 'axios';

export const HTTP = axios.create({
  baseURL: `http://${process.env.API_HOST}:${process.env.API_PORT}/api/`
});
