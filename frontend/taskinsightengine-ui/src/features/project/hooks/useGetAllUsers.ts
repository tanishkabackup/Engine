import { getAllUsers } from "../../auth/services/authservice"
import { GetAllUsersResponse } from "../../../types/response";
import { useQuery } from "@tanstack/react-query";

export const useGetAllUsers = () => {
    return useQuery<GetAllUsersResponse>({
        queryKey: ["users"],
        queryFn: getAllUsers
    });
}