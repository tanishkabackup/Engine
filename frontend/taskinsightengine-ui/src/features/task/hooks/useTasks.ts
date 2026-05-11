import { useMutation, useQuery } from "@tanstack/react-query"
import { AddTaskImpedimentRequest, AssignTaskRequest, CreateRiskSubscriptionRequest, CreateTaskRequest, DailyTaskUpdateRequest, GetDailyTaskUpdateRequest, GetTaskImpedimentCommentsRequest, GetTaskImpedimentRequest } from "../../../types/request";
import { AddTaskImpediment, assignTask, createTask, DailyTaskUpdate, GetDailyTasksUpdate, GetTaskImpedimentComments, GetTaskImpediments } from "../services/taskservice";
import { AddTaskImpedimentResponse, AssignTaskResponse, CreateTaskResponse } from "../../../types/response";
import { CreateRiskSubscription } from "@/features/project/services/projectservice";

export const useCreateTask = () => {
    return useMutation<CreateTaskResponse, Error, CreateTaskRequest>({
        mutationFn: (data) => createTask(data)
    });
}

export const UseDailyTaskUpdate = () => {
    return useMutation<void, Error, DailyTaskUpdateRequest>({
        mutationFn: (data): Promise<void> => DailyTaskUpdate(data)
    });
}

export const useGetDailyTaskUpdates = (request: GetDailyTaskUpdateRequest) => {
    return useQuery({
        queryKey: ["taskUpdates", request.taskId],
        queryFn: () => GetDailyTasksUpdate(request),
        select: (data) => data.dailyTaskUpdates,
    });
}

export const useAddTaskImpediment = () => {
    return useMutation<AddTaskImpedimentResponse, Error, AddTaskImpedimentRequest>({
        mutationFn: (data) => AddTaskImpediment(data)
    });
}

export const useGetImpediments = (request: GetTaskImpedimentRequest) => {
    return useQuery({
        queryKey: ["taskImpediments", request.taskId],
        queryFn: () => GetTaskImpediments(request),
        select: (data) => data.taskImpediments,
    });
}

export const useGetTaskImpedimentComments = (request: GetTaskImpedimentCommentsRequest) => {
    return useQuery({
        queryKey: ["taskImpedimentComments", request.taskImpedimentId],
        queryFn: () => GetTaskImpedimentComments(request),
        select: (data) => data.impedimentComments
    })
}

export const useCreateRiskSubscription = () => {
    return useMutation<void, Error, CreateRiskSubscriptionRequest>
        ({ mutationFn: (data) => CreateRiskSubscription(data) })
}

export const useTaskAssignment = () => {
    return useMutation<AssignTaskResponse, Error, AssignTaskRequest>({
        mutationFn: (data) => assignTask(data)
    });
}