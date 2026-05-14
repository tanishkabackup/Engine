import { useMutation } from "@tanstack/react-query";
import { api } from "../../../services/apiClient";
import { AddProjectMemberRequest, CancelProjectRiskSubscriptionRequest, CreateProjectRequest, CreateProjectRiskSubscriptionRequest, GetAllProjectsRequest, GetProjectDashboardRequest, GetProjectMembersRequest, GetProjectRiskSubscriptionRequest, GetProjectTasksRequest } from "../../../types/request";
import { createProject, createRiskSubscription, getProjectRiskSubscription } from "../services/projectservice";
import { useQuery } from "@tanstack/react-query";
import { getAllProjects, getProjectDashboard, getProjectMembers } from "@/features/project/services/projectservice";
import { GetAllProjectsResponse, GetProjectDashboardResponse, GetProjectMembersResponse, GetProjectRiskSubscriptionResponse, GetProjectTasksResponse } from "@/types/response";
import { getProjectsTasks } from "@/features/task/services/taskservice";

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
        select: (data: GetProjectTasksResponse) => data.taskList,
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
        select: (data: GetAllProjectsResponse) => data.projects,
        enabled: options?.enabled,
    });
};

export const useGetProjectMembers = (
    request: GetProjectMembersRequest) => {
    return useQuery({
        queryKey: ["members", request.email],
        queryFn: () => getProjectMembers(request),
        enabled: !!request.email,
        select: (data: GetProjectMembersResponse) => data.members,
    });
};

export const useCreateRiskSubscription = () => {
    return useMutation<void, Error, CreateProjectRiskSubscriptionRequest>
        ({ mutationFn: (data) => createRiskSubscription(data) })
}

export const useGetProjectRiskSubscription = (
    request: GetProjectRiskSubscriptionRequest,
) => {
    return useQuery({
        queryKey: ["projectRiskSubscriptions", request?.email],
        queryFn: () => getProjectRiskSubscription(request),
        enabled: !!request?.email,
        select: (data: GetProjectRiskSubscriptionResponse) => data.riskSubscriptions
    });
};

export const useGetProjectDashboard = (request: GetProjectDashboardRequest) => {
    return useQuery({
        queryKey: ["projectDashboard", request.email?.toLowerCase],
        queryFn: () => getProjectDashboard(request),
        enabled: !!request.email,
        staleTime: 5 * 60 * 1000,
        gcTime: 30 * 60 * 1000,
        refetchOnWindowFocus: false,
        select: (data: GetProjectDashboardResponse) => data.projectMetrics

    });
}

export const useCancelProjectRiskSubscription = () => {
    return useMutation<void, Error, CancelProjectRiskSubscriptionRequest>
        ({ mutationFn: (data) => api.post("/Project/CancelProjectRiskSubscription", data) })
}