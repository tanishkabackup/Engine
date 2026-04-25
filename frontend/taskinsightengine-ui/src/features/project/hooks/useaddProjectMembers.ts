import { useMutation } from "@tanstack/react-query";
import { api } from "../../../services/apiClient";
import { AddProjectMemberRequest, GetProjectTasksRequest } from "../../../types/request";
import { getProjectsTasks } from "../services/projectservice";
import { useQuery } from "@tanstack/react-query";

export const useAddProjectMembers = () => {
  return useMutation({
    mutationFn: (data: AddProjectMemberRequest) =>
      api.post("/Project/AddProjectMembers", data)
  });
};

export const useGetProjectsTasks=( 
    request: GetProjectTasksRequest
)=>{
    return useQuery({
         queryKey: ["taskList" , request.projectId],
         queryFn: () => getProjectsTasks(request),
         select: (data) => data.taskList,
    });
};


