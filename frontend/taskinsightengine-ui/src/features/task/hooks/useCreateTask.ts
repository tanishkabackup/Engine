import { useMutation, useQuery } from "@tanstack/react-query"
import { AddTaskImpedimentRequest, CreateTaskRequest, DailyTaskUpdateRequest, GetDailyTaskUpdateRequest, GetTaskImpedimentRequest } from "../../../types/request";
import { AddTaskImpediment, createTask, DailyTaskUpdate, GetDailyTasksUpdate, GetTaskImpediments } from "../services/taskservice";
import { AddTaskImpedimentResponse, CreateTaskResponse } from "../../../types/response";

export const useCreateTask = ()  =>{
    return useMutation<CreateTaskResponse,Error,CreateTaskRequest>({
       mutationFn: (data) =>  createTask(data)
    });
}

export const UseDailyTaskUpdate=()=>{
    return useMutation<void,Error,DailyTaskUpdateRequest>({
      mutationFn: (data): Promise<void> => DailyTaskUpdate(data)
    });
}

export const useGetDailyTaskUpdates =(request: GetDailyTaskUpdateRequest) =>{
     return useQuery({
        queryKey: ["taskUpdates",request.taskId],
        queryFn:()=> GetDailyTasksUpdate(request),
        select: (data) => data.dailyTaskUpdates,
     });
}

export const useAddTaskImpediment =() =>{
    return useMutation<AddTaskImpedimentResponse , Error ,AddTaskImpedimentRequest>({
        mutationFn: (data) => AddTaskImpediment(data)
    });
}

export const useGetImpediments =(request: GetTaskImpedimentRequest) =>{
     return useQuery({
        queryKey: ["taskImpediments",request.taskId],
        queryFn:()=> GetTaskImpediments(request),
        select: (data) => data.taskImpediments,
     });
}