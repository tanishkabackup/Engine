
import { useQuery } from "@tanstack/react-query";
import { GetAllProjectsRequest, RegisterRequest } from "../../../types/request";
import { getAllProjects } from "../services/taskservice";

export const useGetAllProjects = (
  request: GetAllProjectsRequest,
  options?: { enabled?: boolean } 
) => {
  return useQuery({
    // Include the IDs in the key so the cache is unique to the request
    queryKey: ["projects", request.projectIds], 
    queryFn: () => getAllProjects(request),
    select: (data) => data.projects,
    enabled: options?.enabled, 
  });
};