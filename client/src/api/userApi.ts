import axios from "axios";

const API_URL = 'http://localhost:5187/api/User';

export const register = async (
    fullName: string,
    email: string,
    dob: Date,
    phoneNumber: string,
    address: string,
    userName: string,
    password: string,
    confirmPassword: string
) => {
    try {
        const response = await axios.post(`${API_URL}/RegisterCustomer`, {
            FullName: fullName,
            Email: email,
            Dob: dob,
            PhoneNumber: phoneNumber,
            Address: address,
            UserName: userName,
            Password: password,
            ConfirmPassword: confirmPassword
        });
        return response.data;
    } catch (error: any) {
        throw new Error(error.response?.data?.message || error.message);
    }
}