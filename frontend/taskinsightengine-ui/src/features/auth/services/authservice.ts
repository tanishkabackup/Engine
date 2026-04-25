
import { api } from "../../../services/apiClient";
import { LoginRequest, RegisterRequest } from "../../../types/request";
import { GetAllUsersResponse, LoginResponse, LogoutResponse } from "../../../types/response";

export const registerUser = async (data: RegisterRequest) :Promise<void> => {
  return await api.post<void>("/User/RegisterUser", data);
  
};

export const getAllUsers = async() : Promise<GetAllUsersResponse> =>{
 return await api.post<GetAllUsersResponse>("/User/GetAllUsers", {});
}

export const loginUser = async(data: LoginRequest): Promise<LoginResponse> =>{
   return await api.post<LoginResponse>("/User/Login", data);
}

export const logoutUser = async() : Promise<LogoutResponse> =>{
  return await api.post<LogoutResponse>("/User/Logout", {});
}

