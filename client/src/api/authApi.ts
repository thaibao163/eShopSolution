import axios from 'axios';

const API_URL = 'http://localhost:5187/api/User';

export interface LoginResponse {
    token: string;
    userName: string;
    message: string;
}

export const login = async (
    logIn: string,
    password: string,
    rememberMe: boolean
): Promise<LoginResponse> => {
    try {
        const response = await axios.post<LoginResponse>(`${API_URL}/Login`, {
            Login: logIn,
            Password: password,
            RememberMe: rememberMe
        });
        return response.data;
    } catch (error: any) {
        throw new Error(
            error.response?.data?.message ||
            error.response?.data?.title ||
            error.message
        );
    }
}

export const logout = async (): Promise<void> => {
    await axios.post(`${API_URL}/Logout`)
}