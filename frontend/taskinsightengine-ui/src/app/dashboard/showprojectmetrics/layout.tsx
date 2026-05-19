"use client";

import { ProjectSidebar } from "@/components/sidebar/ProjectSidebar";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useGetProjectDashboard } from "@/features/project/hooks/useProjects";
import { useParams } from "next/navigation";

export default function ProjectsLayout({ children }: { children: React.ReactNode }) {

    const params = useParams();
    const currentId = params.id ? Number(params.id) : null;

    const { email } = useCurrentUser();

    const { data: projects, isLoading } = useGetProjectDashboard(email ? { email } : null);

    return (
        <div className="flex h-screen bg-gray-50">
            <aside className="w-72 bg-white border-r flex flex-col shadow-sm">
                <div className="p-6 border-b bg-gray-900 text-white">
                    <h1 className="text-xl font-bold tracking-tight">My Active Projects</h1>
                </div>
                <div className="flex-1 overflow-y-auto p-4">
                    {isLoading ? (
                        <div className="space-y-3 animate-pulse">
                            {[1, 2, 3].map((i) => <div key={i} className="h-12 bg-gray-200 rounded" />)}
                        </div>
                    ) : (
                        <ProjectSidebar projects={projects || []} currentProjectId={currentId} />
                    )}
                </div>
            </aside>
            <main className="flex-1 overflow-y-auto">{children}</main>
        </div>

    );
}