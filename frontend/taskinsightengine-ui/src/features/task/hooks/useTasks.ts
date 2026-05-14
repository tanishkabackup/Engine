import { useMutation, useQuery } from "@tanstack/react-query"
import { AddTaskImpedimentRequest, AssignTaskRequest, CreateTaskRequest, DailyTaskUpdateRequest, GetDailyTaskUpdateRequest, GetTaskImpedimentCommentsRequest, GetTaskImpedimentRequest } from "../../../types/request";
import { addTaskImpediment, assignTask, createTask, dailyTaskUpdate, getDailyTasksUpdate, getTaskImpedimentComments, getTaskImpediments} from "../services/taskservice";
import { AddTaskImpedimentResponse, AssignTaskResponse, CreateTaskResponse, GetDailyTaskUpdatesResponse, GetTaskImpedimentCommentsResponse, GetTaskImpedimentResponse } from "../../../types/response";

export const useCreateTask = () => {
    return useMutation<CreateTaskResponse, Error, CreateTaskRequest>({
        mutationFn: (data) => createTask(data)
    });
}

export const UseDailyTaskUpdate = () => {
    return useMutation<void, Error, DailyTaskUpdateRequest>({
        mutationFn: (data): Promise<void> => dailyTaskUpdate(data)
    });
}

export const useGetDailyTaskUpdates = (request: GetDailyTaskUpdateRequest) => {
    return useQuery({
        queryKey: ["taskUpdates", request.taskId],
        queryFn: () => getDailyTasksUpdate(request),
        select: (data:GetDailyTaskUpdatesResponse) => data.dailyTaskUpdates,
    });
}

export const useAddTaskImpediment = () => {
    return useMutation<AddTaskImpedimentResponse, Error, AddTaskImpedimentRequest>({
        mutationFn: (data) => addTaskImpediment(data)
    });
}

export const useGetImpediments = (request: GetTaskImpedimentRequest) => {
    return useQuery({
        queryKey: ["taskImpediments", request.taskId],
        queryFn: () => getTaskImpediments(request),
        select: (data: GetTaskImpedimentResponse) => data.taskImpediments,
    });
}

export const useGetTaskImpedimentComments = (request: GetTaskImpedimentCommentsRequest) => {
    return useQuery({
        queryKey: ["taskImpedimentComments", request.taskImpedimentId],
        queryFn: () => getTaskImpedimentComments(request),
        select: (data:GetTaskImpedimentCommentsResponse) => data.impedimentComments
    })
}

export const useTaskAssignment = () => {
    return useMutation<AssignTaskResponse, Error, AssignTaskRequest>({
        mutationFn: (data) => assignTask(data)
    });
}