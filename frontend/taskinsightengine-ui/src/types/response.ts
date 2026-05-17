import { JSX } from "react/jsx-runtime";
import { BriefingItem, ImpedimentCommentDto } from "./request";

export interface GetAllUsersResponse {
    users: UserDetail[];
}

export interface UserDetail {
    userId: number;
    userName: string;
    email: string;
    role: string;
}

export interface UserLoginDto {
    email: string;
    fullName: string;
    role: string;
}

export interface CreateProjectResponse {
    projectId: number;
}

export interface GetProjectMembersResponse {
    members: MemberDetail[];
}

export interface MemberDetail {
    fullName: string;
    projectId: number;
    memberId: number;
    role: string;
    email: string;
}

export interface ProjectDetail {
    name: string;
    projectId: number;
    description: string;
    startDate: Date;
    closingDate: Date;
    updatedAt: Date;
    priority: string;
    createdAt: Date;
}

export interface GetAllProjectsResponse {
    projects: ProjectDetail[];
}

export interface CreateTaskResponse {
    taskId: number;
}

export interface AssignTaskResponse {
    taskAssignmentId: number;
}

export interface LoginResponse {
    user: UserLoginDto;
}

export interface LogoutResponse {
    isSuccess: boolean;
    message: string;
}

export interface GetProjectTasksResponse {
    taskList: TaskDetail[];
}

export interface TaskDetail {
    taskId: number;
    title: string;
    description: string;
    priorityStatus: string;
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
    status: string;
    updatedEta: Date;
    comment: string;
    effortHours: number;
    lastUpdatedDate: Date;
    projectMemberName: string;
    Email: string;
}

export interface GetDailyTaskUpdatesResponse {
    dailyTaskUpdates: DailyTaskUpdateDetail[];
}

export interface AddTaskImpedimentResponse {
    isSuccess: boolean
    taskImpedimentId: number
}

export interface GetTaskImpedimentResponse {
    taskImpediments: TaskImpedimentDetail[];
}

export interface TaskImpedimentDetail {
    taskImpedimentId: number;
    title: string;
    riskStatus: string;
    isResolved: boolean;
    resolvedBy: string;
    taskId: number;
    riskDescription: string;
    createdBy: string;
    resolvedAt: Date;
    lastUpdated: Date;
    createdAt: Date;
}

export interface GetTaskImpedimentCommentsResponse {
    impedimentComments: ImpedimentCommentDto[];
}

export interface GetProjectDashboardResponse {
    projectMetrics: ProjectMetricsDto[];
}

export interface ProjectMetricsDto {
    projectId: number;
    projectName: string;
    metrics: ProjectHealthMetrics;
    groups: BriefingSnapshotView;
    workload: TeamMemberWorkload[];
}

export interface ProjectHealthMetrics {
    totalTasks: number;
    critical: number;
    warning: number;
    healthy: number;
    recovering: number;
    healthPercentage: number;
}

export interface TeamMemberWorkload {
    assigneeName?: string;
    roleName?: string;
    totalItems: number;
    critical: number;
    warning: number;
    recovering: number;
    healthy: number;
}


export interface BriefingSnapshotView {
    needsAttention: BriefingItem[];
    silentRisk: BriefingItem[];
    recovering: BriefingItem[];
    healthy: RiskSnapshotDto[];
    history: ProjectTaskHistory;
}

export interface RiskSnapshotDto {
    taskId: number;
    projectId: number;
    title?: string;
    assigneeName?: string;
    assigneeRole?: string;
    date: Date;
    prevScore?: number;
    currentScore?: number;
    prevLevel?: string;
    currentLevel?: string;
    delta?: number;
    topReasons?: string;
    movementId: number;
    assigneeId: number;
}

export interface ProjectTaskHistory {
    taskHistory: TaskHistoryDto[];
}

export interface TaskHistoryDto {
    taskId: number;
    title?: string;
    date: Date;
    riskChange?: string;
    impact?: string;
    owner?: string;
    keyInsight?: string;
}

export interface GetProjectRiskSubscriptionResponse 
{
   riskSubscriptions: RiskSubscriptionDto[];
}

export interface RiskSubscriptionDto
{
  riskSubscriptionId:number;
  riskSubscriptionGuid : string;
  userEmail?: string;
  projectId: number;
  createdAt: string;
  nextRun?: string;
  projectName?: string;
}

export interface UpdateTaskImpedimentResponse {
    resolvedAt: Date;
    resolvedBy: string;
    isSuccess: boolean;
}