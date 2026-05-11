import { useMutation } from "@tanstack/react-query";
import { api } from "../../../services/apiClient";
import { AddProjectMemberRequest, CreateProjectRequest, GetAllProjectsRequest, GetProjectMembersRequest, GetProjectTasksRequest } from "../../../types/request";
import { createProject, getProjectsTasks } from "../services/projectservice";
import { useQuery } from "@tanstack/react-query";
import { getAllProjects, getProjectMembers } from "@/features/task/services/taskservice";

export const useAddProjectMembers = () => {
    return useMutation({
        mutationFn: (data: AddProjectMemberRequest) =>
            api.post("/Project/AddProjectMembers", data)
    });
};

export const useGetProjectsTasks = (
    request: GetProjectTasksRequest
) => {
    return useQuery({
        queryKey: ["taskList", request.projectId],
        queryFn: () => getProjectsTasks(request),
        select: (data) => data.taskList,
    });
};

export const useCreateProject = () => {
    return useMutation({
        mutationFn: (data: CreateProjectRequest) => createProject(data)
    });
};

export const useGetAllProjects = (
    request: GetAllProjectsRequest,
    options?: { enabled?: boolean }
) => {
    return useQuery({
        // the IDs in the key so the cache is unique to the request
        queryKey: ["projects", request.projectIds],
        queryFn: () => getAllProjects(request),
        select: (data) => data.projects,
        enabled: options?.enabled,
    });
};

export const useGetProjectMembers = (
    request: GetProjectMembersRequest) => {
    return useQuery({
        queryKey: ["members", request.email],
        queryFn: () => getProjectMembers(request),
        enabled: !!request.email,
        select: (data) => data.members,
    });
};
