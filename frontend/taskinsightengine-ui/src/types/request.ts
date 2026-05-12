export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  role: string;
}


export interface CreateProjectRequest  {
  name: string;
  description: string;
  priority: string;
  startDate: string;
  closeDate: string;
  createdAt: Date;
  updatedAt: Date;
}


export interface AddProjectMemberRequest {
  projectId: number;
  userIds : number[];
  createdAt: Date;
  updatedAt: Date;
}

export interface GetAllProjectsRequest {
  projectIds:number[];
}


export interface GetProjectMembersRequest{
   email: string;
}

export interface CreateTaskRequest{
   projectId: number;
   priority: string;
   title: string;
   description: string ;
   hours: number;
   expectedEta: Date;
   createdAt: Date;
   updatedAt: Date;
}

export interface AssignTaskDetail{
  taskId: number;
  assigneeId: number;
  managerId: number;
  assignerId: number;
  OpeningDate: Date;
  ClosingDate: Date;
}

export interface AssignTaskRequest{
  taskAssignments:AssignTaskDetail[];
}

export interface LoginRequest  {
   email: string 
   password: string;
}

export interface GetProjectTasksRequest {
  projectId: number;
}

export interface DailyTaskUpdateRequest{
  taskId:number;
  status: string ;
  updatedEta: Date;
  comment: string;
  effortHours?: number;
  projectMemberId: number;
}

export interface GetDailyTaskUpdateRequest{
  taskId: number;
}

export interface AddTaskImpedimentRequest{
    title: string;
    riskStatus: string;
    isResolved: boolean;
    resolvedBy : string;
    taskId: number;
    riskDescription:string;
    createdBy: string;
}

export interface GetTaskImpedimentRequest
{
  taskId:number;
}

export interface SendImpedimentCommentRequest
{
  taskImpedimentId : number;
  message: string;
}

export interface ImpedimentCommentDto
{
  commentId : number;
  fullName: string;
  email: string;
  createdAt: Date ;
  message: string;

}

export interface GetTaskImpedimentCommentsRequest
{
   taskImpedimentId : number;
}

export interface CreateRiskSubscriptionRequest{
   hours: number;
   minutes: number;
   projectIds: number[];
   email: string;
}

export interface BriefingItem {
  taskId: number;
  title: string ;
  level: string ;
  score: number ;
  delta: number ;
  movement: string ;
  topReasons: string ;
  assigneeId: number;
  projectId: number;
}

export interface BriefingSnapshot {
  needsAttention: BriefingItem[];
  slientRisk: BriefingItem[]; 
  recovering: BriefingItem[];
}
