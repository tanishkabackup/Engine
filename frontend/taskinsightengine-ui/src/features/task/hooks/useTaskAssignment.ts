import { useMutation } from "@tanstack/react-query";
import { assignTask } from "../services/taskservice";
import { AssignTaskResponse } from "../../../types/response";
import { AssignTaskRequest } from "../../../types/request";

export const useTaskAssignment = () => {
    return useMutation<AssignTaskResponse, Error, AssignTaskRequest>({
        mutationFn: (data)=> assignTask(data)
    });
}