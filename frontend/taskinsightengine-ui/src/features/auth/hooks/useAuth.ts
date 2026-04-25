import { useMutation } from "@tanstack/react-query";
import { loginUser, logoutUser, registerUser} from "../services/authservice";
import { LoginRequest, RegisterRequest } from "../../../types/request";
import { LogoutResponse, UserLoginDto } from "../../../types/response";
import { useRouter } from "next/navigation";

export function useRegister () {
  return useMutation({
    mutationFn: (data: RegisterRequest) => registerUser(data)
  });
};

export function useLogin()
{
  const router = useRouter();
    return useMutation({
      mutationFn:(data:LoginRequest)=> loginUser(data),

      onSuccess: (response) =>{
        sessionStorage.setItem("user", JSON.stringify(response.user));
        router.push("/dashboard/createproject");
      }
    });
}

export function useCurrentUser(): UserLoginDto | null {
  if (typeof window === 'undefined') {
    return null; 
  }
  const saved = sessionStorage.getItem('user');
  console.log("Current User from sessionStorage:", saved);
  return saved ? (JSON.parse(saved) as UserLoginDto) : null;
}

export function useLogout()
{
   return useMutation({
       mutationFn: () => logoutUser(),
       onSuccess: (response: LogoutResponse) => {
          if(response.isSuccess)
          {
            window.location.replace('/account/login');
            sessionStorage.removeItem("user");
            localStorage.clear();
          }
       }
   });
} 