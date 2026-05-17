import { api } from "../../../services/apiClient";
import { AddTaskImpedimentRequest, AssignTaskRequest, CreateTaskRequest, DailyTaskUpdateRequest, GetAllProjectsRequest, GetDailyTaskUpdateRequest, GetProjectMembersRequest, GetProjectTasksRequest, GetTaskImpedimentCommentsRequest, GetTaskImpedimentRequest, UpdateTaskImpedimentRequest } from "../../../types/request";
import { AddTaskImpedimentResponse, AssignTaskResponse, CreateTaskResponse, GetAllProjectsResponse, GetDailyTaskUpdatesResponse, GetProjectMembersResponse, GetProjectTasksResponse, GetTaskImpedimentCommentsResponse, GetTaskImpedimentResponse, UpdateTaskImpedimentResponse } from "../../../types/response";

export const getProjectsTasks = async (data: GetProjectTasksRequest) :Promise<GetProjectTasksResponse> => 
{
   return await api.post<GetProjectTasksResponse>("/Project/GetProjectTasks", data);
}

export const createTask = async (data: CreateTaskRequest) :Promise<CreateTaskResponse> =>{
    return await api.post<CreateTaskResponse>("/Task/CreateTask", data);
}

export const assignTask = async(data: AssignTaskRequest) : Promise<AssignTaskResponse> =>{
    return await api.post<AssignTaskResponse>("/Task/AssignTask", data);
    
}

export const dailyTaskUpdate = async(data: DailyTaskUpdateRequest) : Promise<void> =>{
    return await api.post("/Task/DailyTaskUpdate", data);
}

export const getDailyTasksUpdate = async(data: GetDailyTaskUpdateRequest) : Promise<GetDailyTaskUpdatesResponse> =>{
    return await api.post<GetDailyTaskUpdatesResponse>("/Task/GetDailyTaskUpdates", data);
}

export const addTaskImpediment = async(data:AddTaskImpedimentRequest): Promise<AddTaskImpedimentResponse>=>
{
    return await api.post("/Task/AddTaskImpediment",data);
}

export const getTaskImpediments = async(data:GetTaskImpedimentRequest): Promise<GetTaskImpedimentResponse>=>
{
    return await api.post("/Task/GetTaskImpediments",data)
}

export const getTaskImpedimentComments = async(data:GetTaskImpedimentCommentsRequest): Promise<GetTaskImpedimentCommentsResponse>=>
{
    return await api.post("/Task/GetTaskImpedimentComments",data)
}

export const updateTaskImpediment = async(data:UpdateTaskImpedimentRequest): Promise<UpdateTaskImpedimentResponse>=>
{
    return await api.post("/Task/UpdateTaskImpediment",data)
}