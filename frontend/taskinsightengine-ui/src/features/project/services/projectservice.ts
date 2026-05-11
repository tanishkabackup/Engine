
import { api } from "../../../services/apiClient";
import { CreateProjectRequest, CreateRiskSubscriptionRequest, GetProjectTasksRequest } from "../../../types/request";
import { CreateProjectResponse, GetProjectTasksResponse} from "../../../types/response";

export const createProject = async (data: CreateProjectRequest) :Promise<CreateProjectResponse> => {
  return await api.post<CreateProjectResponse>("/Project/CreateProject", data);
  
};

export const getProjectsTasks = async (data: GetProjectTasksRequest) :Promise<GetProjectTasksResponse> => 
{
   return await api.post<GetProjectTasksResponse>("/Project/GetProjectTasks", data);
};

export const CreateRiskSubscription = async(data: CreateRiskSubscriptionRequest):Promise<void> =>
{
    return await api.post("/Project/CreateRiskSubscription",data);
}