"use client";

import { useParams } from "next/navigation";
import { useGetProjectDashboard } from "@/features/project/hooks/useProjects";
import { HealthGauge } from "@/components/metrics/HealthGauge";
import { WorkLoadTable } from "@/components/metrics/WorkLoadTable";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { TaskHistoryDto, RiskSnapshotDto } from "@/types/response";

export default function ProjectDetailPage() {
    const { id } = useParams();
    const { email } = useCurrentUser();
    const { data: projects } = useGetProjectDashboard(email ? { email } : null);


    const project = projects?.find(p => p.projectId === Number(id));

    if (!project) return <div className="p-10 text-gray-400">Project data not available.</div>;

    return (
        <div className="p-8 max-w-6xl mx-auto space-y-8 animate-in fade-in duration-500">
            <div className="flex justify-between items-center bg-white p-8 rounded-2xl shadow-sm border border-gray-100">
                <div>
                    <h2 className="text-4xl font-black text-gray-900">{project.projectName}</h2>
                    <p className="text-gray-500 mt-2">Active Monitoring & Insights</p>
                </div>
                <HealthGauge percentage={project.metrics.healthPercentage} projectMetrics={project} />
            </div>

            <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                <div className="lg:col-span-2 space-y-8">
                    <section>
                        <h3 className="text-lg font-bold text-gray-800 mb-4">Resource Allocation</h3>
                        <WorkLoadTable workload={project.workload} />
                    </section>
                </div>
            </div>

            <div className="space-y-6">
                <h3 className="text-lg font-bold text-gray-800">Critical Risks</h3>
                {project.groups.needsAttention.map((item) => (
                    <div key={item.taskId} className="p-4 bg-white border-l-4 border-red-500 rounded-lg shadow-sm">
                        <p className="font-bold text-gray-900">{item.title}</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.topReasons}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.level}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.movement}"</p>
                    </div>
                ))}

                {project.groups.recovering.map((item) => (
                    <div key={item.taskId} className="p-4 bg-white border-l-4 border-amber-400 rounded-lg shadow-sm">
                        <p className="font-bold text-gray-900">{item.title}</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.topReasons}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.level}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.movement}"</p>
                    </div>
                ))}

                {project.groups.silentRisk.map((item) => (
                    <div key={item.taskId} className="p-4 bg-white border-l-4 border-amber-400 rounded-lg shadow-sm">
                        <p className="font-bold text-gray-900">{item.title}</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.topReasons}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.level}"</p>
                        <p className="text-sm text-gray-500 mt-2 italic">"{item.movement}"</p>
                    </div>
                ))}

                {/* Healthy Tasks */}
                <section className="space-y-4">
                    <h3 className="text-lg font-bold text-gray-800">Healthy Tasks</h3>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                        {project.groups.healthy.map((task: RiskSnapshotDto) => (
                            <div key={task.taskId} className="p-4 bg-white border border-gray-100 rounded-xl shadow-sm flex justify-between items-center">
                                <div>
                                    <p className="font-semibold text-gray-900">{task.title}</p>
                                    <p className="text-xs text-gray-500">{task.assigneeName} • {task.assigneeRole}</p>
                                </div>
                                <div className="flex flex-col items-end">
                                    <span className="text-xs font-bold text-green-600 bg-green-50 px-2 py-1 rounded">Healthy</span>
                                    <p className="text-[10px] text-gray-400 mt-1">Score: {task.currentScore}</p>
                                </div>
                            </div>
                        ))}
                    </div>
                </section>

                {/* History Timeline */}
                <section className="space-y-6">
                    <h3 className="text-lg font-bold text-gray-800">Project Insight History</h3>
                    <div className="relative border-l-2 border-gray-100 ml-3 space-y-8">
                        {project.groups.history.taskHistory.map((entry: TaskHistoryDto, index: number) => (
                            <div key={`${entry.taskId}-${index}`} className="relative pl-8">

                                <div className="absolute -left-[9px] top-1 h-4 w-4 rounded-full border-2 border-white bg-blue-500 shadow-sm" />

                                <div className="flex flex-col space-y-1">
                                    <div className="flex items-center justify-between">
                                        <span className="text-xs font-bold text-gray-400 uppercase tracking-wider">
                                            {new Date(entry.date).toLocaleDateString()}
                                        </span>
                                        <span className={`text-[10px] px-2 py-0.5 rounded-full font-bold ${entry.impact === "Escalated" ? "bg-red-100 text-red-600" : "bg-gray-100 text-gray-600"
                                            }`}>
                                            {entry.impact}
                                        </span>
                                    </div>
                                    <h4 className="font-bold text-gray-900">{entry.title}</h4>
                                    <p className="text-sm text-blue-600 font-medium">{entry.riskChange}</p>
                                    <p className="text-sm text-gray-600 leading-relaxed bg-gray-50 p-3 rounded-lg border border-gray-100 italic">
                                        "{entry.keyInsight}"
                                    </p>
                                    <p className="text-xs text-gray-400 mt-1">AssignedTo: {entry.owner}</p>
                                </div>
                            </div>
                        ))}
                    </div>
                </section>

            </div>
        </div>
    )
}