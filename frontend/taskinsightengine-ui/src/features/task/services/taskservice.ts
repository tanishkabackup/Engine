import { api } from "../../../services/apiClient";
import { AddTaskImpedimentRequest, AssignTaskRequest, CreateTaskRequest, DailyTaskUpdateRequest, GetAllProjectsRequest, GetDailyTaskUpdateRequest, GetProjectMembersRequest, GetTaskImpedimentCommentsRequest, GetTaskImpedimentRequest } from "../../../types/request";
import { AddTaskImpedimentResponse, AssignTaskResponse, CreateTaskResponse, GetAllProjectsResponse, GetDailyTaskUpdatesResponse, GetProjectMembersResponse, GetTaskImpedimentCommentsResponse, GetTaskImpedimentResponse } from "../../../types/response";

export const getProjectMembers = async(data :GetProjectMembersRequest) : Promise<GetProjectMembersResponse> =>{
  return await api.post<GetProjectMembersResponse>("/Project/GetAllProjectMembers",data);
  
}

export const getAllProjects = async(data: GetAllProjectsRequest) : Promise<GetAllProjectsResponse> => {
    return await api.post("/Project/GetAllProjectDetails", data);
    
}

export const createTask = async (data: CreateTaskRequest) :Promise<CreateTaskResponse> =>{
    return await api.post<CreateTaskResponse>("/Task/CreateTask", data);
}

export const assignTask = async(data: AssignTaskRequest) : Promise<AssignTaskResponse> =>{
    return await api.post<AssignTaskResponse>("/Task/AssignTask", data);
    
}

export const DailyTaskUpdate = async(data: DailyTaskUpdateRequest) : Promise<void> =>{
    return await api.post("/Task/DailyTaskUpdate", data);
}

export const GetDailyTasksUpdate = async(data: GetDailyTaskUpdateRequest) : Promise<GetDailyTaskUpdatesResponse> =>{
    return await api.post<GetDailyTaskUpdatesResponse>("/Task/GetDailyTaskUpdates", data);
}

export const AddTaskImpediment = async(data:AddTaskImpedimentRequest): Promise<AddTaskImpedimentResponse>=>
{
    return await api.post("/Task/AddTaskImpediment",data);
}

export const GetTaskImpediments = async(data:GetTaskImpedimentRequest): Promise<GetTaskImpedimentResponse>=>
{
    return await api.post("/Task/GetTaskImpediments",data)
}

export const GetTaskImpedimentComments = async(data:GetTaskImpedimentCommentsRequest): Promise<GetTaskImpedimentCommentsResponse>=>
{
    return await api.post("/Task/GetTaskImpedimentComments",data)
}