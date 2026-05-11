"use client";

import { useGetProjectsTasks } from "../hooks/useProjects";
import { GetProjectTasksRequest } from "../../../types/request";
import { useState } from "react";
import UpdateTaskForm from "./UpdateTaskForm";
import { Priority } from "@/types/constants";
interface ShowTasksProps {
    projectId: number;
    memberId: number;
    onBack: () => void;
}

export default function ShowTasks({ projectId, memberId, onBack }: ShowTasksProps) {

    const [activeTask, setActiveTask] = useState<any | null>(null);

    const { data: taskList, isLoading } = useGetProjectsTasks({ projectId: Number(projectId) } as GetProjectTasksRequest);

    if (activeTask) {
        return (
            <UpdateTaskForm
                task={activeTask}
                memberId={memberId}
                onBack={() => setActiveTask(null)}
            />
        );

    }

    console.log("Active Task Data:", activeTask);

    return (
        <div className="max-w-5xl mx-auto p-6">
            {/* Navigation Header */}
            <div className="flex items-center justify-between mb-8">
                <button
                    onClick={onBack}
                    className="group flex items-center gap-2 text-slate-500 hover:text-sky-600 font-bold transition-colors"
                >
                    <div className="p-2 bg-slate-100 group-hover:bg-sky-50 rounded-lg">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={3} stroke="currentColor" className="w-4 h-4">
                            <path strokeLinecap="round" strokeLinejoin="round" d="M10.5 19.5 3 12m0 0 7.5-7.5M3 12h18" />
                        </svg>
                    </div>
                    Back to Projects
                </button>
            </div>

            <h2 className="text-3xl font-black text-slate-900 mb-6">Project Roadmap</h2>

            {/* Task List Container */}
            <div className="space-y-4">
                {isLoading ? (
                    <div className="text-center py-20 bg-white border border-slate-200 rounded-2xl">
                        <div className="w-10 h-10 border-4 border-sky-600 border-t-transparent rounded-full animate-spin mx-auto mb-4"></div>
                        <p className="text-slate-500 font-bold">Loading tasks...</p>
                    </div>
                ) : taskList?.length > 0 ? (
                    taskList.map((task: any) => (
                        <div
                            key={task.createdAt}
                            className="bg-white border border-slate-200 p-5 rounded-2xl hover:border-sky-200 hover:shadow-md transition-all flex flex-col md:flex-row md:items-center justify-between gap-4"
                        >
                            <div className="flex-1">
                                <div className="flex items-center gap-3 mb-1">
                                    <h3 className="font-bold text-slate-800 text-lg">{task.title}</h3>
                                    <span className={`text-[10px] font-bold px-2 py-0.5 rounded-md border ${task.priorityStatus === Priority.High ? 'bg-red-50 text-red-600 border-red-100' : 'bg-slate-50 text-slate-500 border-slate-200'
                                        }`}>
                                        {task.priorityStatus}
                                    </span>
                                </div>
                                <p className="text-sm text-slate-500 line-clamp-1">{task.description}</p>
                            </div>

                            <div className="flex items-center gap-6">
                                {/* Hours Metric */}
                                <div className="text-right">
                                    <p className="text-[10px] font-black text-slate-400 uppercase">Effort</p>
                                    <p className="font-bold text-slate-700">{task.hours} hrs</p>
                                </div>

                                {/* ETA Metric */}
                                <div className="text-right border-l border-slate-100 pl-6">
                                    <p className="text-[10px] font-black text-slate-400 uppercase">Deadline</p>
                                    <p className="font-bold text-slate-700">
                                        {new Date(task.expectedEta).toLocaleDateString()}
                                    </p>
                                </div>

                                <div className="text-right border-l border-slate-100 pl-6">
                                    <p className="text-[10px] font-black text-slate-400 uppercase">Created At</p>
                                    <p className="font-bold text-slate-700">
                                        {new Date(task.createdAt).toLocaleDateString()}
                                    </p>
                                </div>

                                <button
                                    onClick={() => setActiveTask(task)}
                                    className="font-bold text-sky-600"
                                >
                                    View details
                                </button>
                            </div>
                        </div>
                    ))
                ) : (
                    <div className="text-center py-20 bg-slate-50 border-2 border-dashed border-slate-200 rounded-2xl">
                        <p className="text-slate-500 font-medium">No tasks found for this project.</p>
                    </div>
                )}
            </div>
        </div>
    );
}