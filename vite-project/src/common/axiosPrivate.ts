import axios from "axios";
import authService from "../components/authorization/AuthorizeService";

const axiosPrivate = axios.create({
    baseURL: "https://localhost:7121"
});

axiosPrivate.interceptors.request.use(async (config) => {
    const token = await authService.getAccessToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
},
    (error) => Promise.reject(error)    
);

export { axiosPrivate };