import { JSX } from "react/jsx-runtime";

export interface GetAllUsersResponse {
       users: UserDetail[];
}

export interface UserDetail{
    userId: number;
    userName: string;
    email: string;
    role: string;
}

export interface UserLoginDto
{
    email: string;
    fullName: string;
    role: string;
}

export interface CreateProjectResponse {
    projectId: number;
}

export interface GetProjectMembersResponse
{
    members: MemberDetail[];
}

export interface MemberDetail
{
    fullName: string ;
    projectId: number;
    memberId: number;
    role: string;
    email:string;
}

export interface ProjectDetail{
    name: string;
    projectId: number;
    description: string;
    startDate: Date;
    closingDate: Date;
    updatedAt: Date;
    priority: string;
    createdAt: Date;
}

export interface GetAllProjectsResponse{
    projects: ProjectDetail[];
}

export interface CreateTaskResponse{
    taskId: number;
}

export interface AssignTaskResponse{
    taskAssignmentId: number;
}

export interface LoginResponse{
   user:UserLoginDto;
}

export interface LogoutResponse{
    isSuccess: boolean;
    message: string;
}

export interface GetProjectTasksResponse{
    taskList: TaskDetail[];
}

export interface TaskDetail{
    taskId: number;
    title : string ;
    description: string;
    priorityId: string;
    hours: number;
    expectedEta: Date;
    createdAt: Date;
    updatedAt: Date;
    allAssigners: string;
    allAssignees: string;
    allManagers: string;
}

export interface DailyTaskUpdateDetail {
    taskId: number;
    status: string ;
    updatedEta: Date;
    comment: string;
    effortHours: number;
    lastUpdatedDate: Date;
    projectMemberName: string;
    Email:string;
}

export interface GetDailyTaskUpdatesResponse
{
    dailyTaskUpdates: DailyTaskUpdateDetail[];
}

export interface AddTaskImpedimentResponse {
    isSuccess:boolean
    taskImpedimentId: number
}

export interface GetTaskImpedimentResponse {
    taskImpediments: TaskImpedimentDetail[];
}

export interface TaskImpedimentDetail
{
    taskImpedimentId: number;
    title: string;
    riskStatus: string;
    isResolved: boolean;
    resolvedBy : string;
    taskId: number;
    riskDescription:string;
    createdBy: string;
    lastUpdated: Date;
    createdAt:Date;
}