import Link from "next/link";
import { ProjectMetricsDto } from "@/types/response";

interface ProjectSidebarProps {
    projects: ProjectMetricsDto[];
    currentProjectId: number | null;
}

export const ProjectSidebar = ({ projects, currentProjectId }: ProjectSidebarProps) => {
    if (!projects || projects.length === 0) {
        return <p className="text-gray-400 text-sm p-4">No projects scheduled for automation.</p>;
    }

    return (
        <nav className="space-y-1">
            {projects.map((project) => (
                <Link
                    key={project.projectId}
                    href={`/dashboard/showprojectmetrics/${project.projectId}`}
                    className={`group flex flex-col p-3 rounded-lg transition-all border ${currentProjectId === project.projectId
                            ? "bg-blue-600 border-blue-600 text-white shadow-md"
                            : "bg-white border-transparent hover:border-gray-200 text-gray-700"
                        }`}
                >
                    <span className="font-bold truncate">{project.projectName}</span>
                    <div className="flex justify-between items-center mt-1">
                        <span
                            className={`text-[12px] ${currentProjectId === project.projectId ? "text-blue-100" : "text-gray-400"
                                }`}
                        >
                            Health: {project.metrics.healthPercentage}%
                        </span>

                        {project.metrics.critical > 0 && (
                            <span className="bg-red-500 text-white text-[10px] px-1.5 py-0.5 rounded-full font-bold">
                                {project.metrics.critical}
                            </span>
                        )}
                    </div>
                </Link>
            ))}
        </nav>
    );
};