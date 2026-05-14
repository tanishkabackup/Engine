"use client";

import { useState } from "react";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useGetUserProjects } from "../hooks/useGetUserProjects";
import ShowTasks from "@/features/task/components/ShowTasksForm";

export default function ShowProjectsForm() {
    const { email: currentUserEmail } = useCurrentUser();

    const [selectedProjectId, setSelectedProjectId] = useState<number | null>(null);

    const { memberRecords, isLoading, projects: sortedProjects } = useGetUserProjects(currentUserEmail);

    if (selectedProjectId !== null) {
        return (
            <ShowTasks
                projectId={selectedProjectId}
                memberId={memberRecords?.find(m => m.email === currentUserEmail)?.memberId || 0}
                onBack={() => setSelectedProjectId(null)}
            />
        );
    }

    if (isLoading) {
        return (
            <div className="flex flex-col items-center justify-center p-20 text-slate-400">
                <div className="w-8 h-8 border-4 border-sky-600 border-t-transparent rounded-full animate-spin mb-4"></div>
                <p className="text-sm font-bold uppercase tracking-widest">Loading Workspace...</p>
            </div>
        );
    }

    return (
        <div className="max-w-6xl mx-auto p-6">
            <div className="mb-10">
                <h1 className="text-3xl font-black text-slate-900 flex items-center gap-3">
                    My Projects
                </h1>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {sortedProjects.length > 0 ? (
                    sortedProjects.map((project) => (
                        <div
                            key={project.projectId}
                            className="group bg-white border border-slate-200 rounded-2xl p-6 hover:shadow-xl hover:border-sky-300 transition-all duration-300 flex flex-col"
                        >
                            <div className="flex-grow">
                                <h3 className="text-xl font-bold text-slate-800 mb-2">{project.name}</h3>
                                <p className="text-sm text-slate-500 line-clamp-2">{project.description}</p>
                            </div>

                            <div className="flex items-center justify-between pt-6 mt-4 border-t border-slate-100">
                                <div className="flex flex-col gap-1">
                                <div className="text-[10px] text-slate-400 font-bold uppercase">
                                    Start Date: {new Date(project.startDate).toLocaleDateString()}
                                </div>
                                <div className="text-[10px] text-slate-400 font-bold uppercase">
                                    Due Date: {new Date(project.closingDate).toLocaleDateString()}
                                </div>
                                </div>

                                <button
                                    onClick={() => setSelectedProjectId(Number(project.projectId))}
                                    className="text-sky-600 font-bold text-xs hover:underline"
                                >
                                    VIEW TASKS →
                                </button>
                            </div>
                        </div>
                    ))
                ) : (
                    <div className="col-span-full text-center py-20 text-slate-400">
                        No projects assigned to this email.
                    </div>
                )}
            </div>
        </div>
    );
}