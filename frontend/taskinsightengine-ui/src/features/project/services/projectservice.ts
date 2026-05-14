
import { api } from "../../../services/apiClient";
import { CancelProjectRiskSubscriptionRequest, CreateProjectRequest, CreateProjectRiskSubscriptionRequest, GetAllProjectsRequest, GetProjectDashboardRequest, GetProjectMembersRequest, GetProjectRiskSubscriptionRequest} from "../../../types/request";
import { CreateProjectResponse, GetAllProjectsResponse, GetProjectDashboardResponse, GetProjectMembersResponse, GetProjectRiskSubscriptionResponse} from "../../../types/response";

export const createProject = async (data: CreateProjectRequest) :Promise<CreateProjectResponse> => {
  return await api.post<CreateProjectResponse>("/Project/CreateProject", data);
  
}

export const getProjectMembers = async(data :GetProjectMembersRequest) : Promise<GetProjectMembersResponse> =>{
  return await api.post<GetProjectMembersResponse>("/Project/GetAllProjectMembers",data);
  
}

export const getAllProjects = async(data: GetAllProjectsRequest) : Promise<GetAllProjectsResponse> => {
    return await api.post("/Project/GetAllProjectDetails", data);
    
}

export const createRiskSubscription = async(data: CreateProjectRiskSubscriptionRequest):Promise<void> =>
{
    return await api.post("/Project/CreateProjectRiskSubscription",data);
}

export const getProjectRiskSubscription = async(data: GetProjectRiskSubscriptionRequest):Promise<GetProjectRiskSubscriptionResponse> =>
{
    return await api.post("/Project/GetProjectRiskSubscription",data);
}

export const getProjectDashboard = async(data:GetProjectDashboardRequest): Promise<GetProjectDashboardResponse> =>
{
    return await api.post("/Project/GetProjectDashboard",data);
}

export const cancelProjectRiskSubscription = async(data: CancelProjectRiskSubscriptionRequest): Promise<void> =>
{
    return await api.post("/Project/CancelProjectRiskSubscription",data);
}