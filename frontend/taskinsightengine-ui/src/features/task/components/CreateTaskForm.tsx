"use client";

import { useForm } from "react-hook-form";
import { useCreateTask } from "../hooks/useCreateTask";
import { useGetAllProjects } from "../hooks/useGetAllProjects";
import { useGetProjectMembers } from "../hooks/useGetProjectMembers";
import { useTaskAssignment } from "../hooks/useTaskAssignment";
import { CreateTaskRequest, GetProjectMembersRequest,AssignTaskRequest } from "../../../types/request";
import { Roles } from "../../../types/constants";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useMemo } from "react";

type CreateTaskFormData = {
  title: string;
  description: string;
  priority: string;
  projectId: number;
  expectedEta: string; 
  hours: number;
  assignedUsers: number[];
};

export default function CreateTaskForm() {
  const { register, handleSubmit, reset, watch } =
    useForm<CreateTaskFormData>();

  const selectedProjectId = watch("projectId");

  const { mutate: createTask } = useCreateTask();

  const { mutate: assignTask } = useTaskAssignment();


  const {email: currentUserEmail} = useCurrentUser();

  const { data : projectData} = useGetProjectMembers({
    email: currentUserEmail,
  } as GetProjectMembersRequest);


 const projectIds = useMemo(() => {

  if (!projectData || projectData.length === 0) {
    return []; 
  }
  
  return [...new Set(projectData.map(item => item.projectId))];
}, [projectData]); 


const { data: projects } = useGetAllProjects(
  { projectIds }, 
  { 
    enabled: projectIds.length > 0,
  }
);

  const members = useMemo(() => {
  return projectData?.filter(p => p.projectId === Number(selectedProjectId)) || [];
}, [projectData, selectedProjectId]);

  const onSubmit = (data: CreateTaskFormData) => {
    const taskData: CreateTaskRequest = {
      title: data.title,
      description: data.description,
      priority: data.priority,
      projectId: Number(data.projectId),
      hours: Number(data.hours),
      expectedEta: data.expectedEta
        ? new Date(data.expectedEta + "T00:00:00")
        : null,
        createdAt: new Date(),
        updatedAt: new Date()
    };

    createTask(taskData, {
    onSuccess: (taskResponse) => {

    const selectedIds = data.assignedUsers.map(Number);

    if (selectedIds.length > 0) {
    
    const assignedMemberDetails = members.filter(m => selectedIds.includes(m.memberId));

    const assignerId = members.find(m => m.email === currentUserEmail)?.memberId;
    const managerId = assignedMemberDetails.find(m => m.role === Roles.ProjectManager)?.memberId || assignerId;
    
    const formTaskAssigmentRequest: AssignTaskRequest = {
      taskAssignments: assignedMemberDetails.map((member) => ({
        taskId: taskResponse.taskId,
        assigneeId: member.memberId,
        assignerId: assignerId, 
        managerId: managerId, 
        OpeningDate: new Date(),
        ClosingDate: new Date(),
      })),
    };

    assignTask(formTaskAssigmentRequest);
  }
  reset();
},
});
  };

  return (
    <div className="max-w-2xl mx-auto bg-white shadow-lg rounded-2xl p-6">
      <h2 className="text-2xl font-bold mb-6 text-gray-800">
        Create New Task
      </h2>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
        {/* Project */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Project
          </label>
          <select
            {...register("projectId", { required: true })}
            className="w-full border border-gray-300 p-2 rounded-lg"
          >
            <option value="">Select Project</option>

            {projects?.map((project) => (
              <option key={project.projectId} value={project.projectId}>
                {project.name}
              </option>
            ))}
          </select>
        </div>

        {/* Title */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Title
          </label>
          <input
            {...register("title", { required: true })}
            className="w-full border border-gray-300 p-2 rounded-lg"
          />
        </div>

        {/* Description */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Description
          </label>
          <textarea
            {...register("description")}
            className="w-full border border-gray-300 p-2 rounded-lg"
          />
        </div>

        {/* Priority */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Priority
          </label>
          <select
            {...register("priority")}
            className="w-full border border-gray-300 p-2 rounded-lg"
          >
            <option value="">Select Priority</option>
            <option value="Low">Low</option>
            <option value="Medium">Medium</option>
            <option value="High">High</option>
          </select>
        </div>

        {/* Expected ETA */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Expected ETA
          </label>
          <input
            type="date"
            {...register("expectedEta")}
            className="w-full border border-gray-300 p-2 rounded-lg"
          />
        </div>

        {/* Estimated Hours */}
        <div>
          <label className="block text-sm font-medium mb-1">
            Estimated Hours
          </label>
          <input
            type="number"
            {...register("hours")}
            placeholder="Estimated hours"
            className="w-full border border-gray-300 p-2 rounded-lg"
          />
        </div>

        {/* Members */}
        <div className="space-y-3">
        <label className="block text-sm font-medium text-gray-700">
         Assign Members
        </label>

        <div className="w-full border border-gray-300 rounded-lg overflow-hidden bg-white">
        <div className="max-h-48 overflow-y-auto p-2 space-y-1">
      
        {/* State: No Project Selected */}
        {!selectedProjectId && (
        <p className="text-sm text-gray-400 p-2 italic text-center">
          Please select a project first...
        </p>
       )}

      {selectedProjectId && (!members || members.length === 0) && (
        <p className="text-sm text-gray-400 p-2 text-center">
          No members found for this project.
        </p>
      )}

      {members?.map((user) => (
        <label 
          key={user.memberId}
          className="flex items-center gap-3 p-2 rounded-md hover:bg-gray-50 cursor-pointer transition-colors border border-transparent hover:border-gray-200"
        >
          <input
            type="checkbox"
            value={user.memberId}
            {...register("assignedUsers")} 
            className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
          />
           <div className="flex flex-col">
            <span className="text-sm font-medium text-gray-900">{user.fullName}</span>
            <span className="text-xs text-gray-500">{user.role}</span>
          </div>
         </label>
         ))}
         </div>
        </div>
      </div>

        {/* Submit */}
        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 rounded-lg"
        >
          Create Task
        </button>
      </form>
    </div>
  );
}
