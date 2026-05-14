"use client";

import { useCancelProjectRiskSubscription, useGetProjectDashboard, useGetProjectRiskSubscription } from "@/features/project/hooks/useProjects";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import Link from "next/link";
import { RiskSubscriptionDto } from "@/types/response";
import { useEffect, useState } from "react";


export default function ProjectOverviewPage() {
    const { email } = useCurrentUser();
    const { data: projects, isLoading } = useGetProjectDashboard(email ? { email } : null);
    const { data: projectRiskSubscriptions } = useGetProjectRiskSubscription(email ? { email } : null);
    const {mutate: cancelSubscription} = useCancelProjectRiskSubscription();
    const [activeSubscriptions, setActiveSubscriptions] = useState<RiskSubscriptionDto[]>([]);

    const handleCancelSubscription = (subscription: RiskSubscriptionDto) => {
         const updatedSubscriptions= [subscription];


         cancelSubscription({subscriptions: updatedSubscriptions },
            {
                onSuccess:() =>{
                    setActiveSubscriptions(prev=> prev.filter(s => s.riskSubscriptionId !== subscription.riskSubscriptionId));
                }
            }
         );
    };

    useEffect( ()=>{
        if(projectRiskSubscriptions)
        {
            setActiveSubscriptions(projectRiskSubscriptions);
        }
    }, [projectRiskSubscriptions]);

    if (isLoading) return <div className="p-10 animate-pulse text-gray-400 font-medium">Loading portfolio...</div>;

    const totalProjects = projects?.length || 0;
    const criticalProjects = projects?.filter(p => p.metrics.critical > 0).length || 0;
   


    return (
        <div className="p-8 space-y-8 animate-in fade-in duration-500">

            <div className="flex justify-between items-end">
            <div>
              <h1 className="text-3xl font-black text-gray-900 tracking-tight">Project Portfolio</h1>
              <p className="text-sm text-gray-500 mt-1">
              Metrics are from last scheduled automated run
              </p>
            </div>
            </div>

            {activeSubscriptions.length>0 ? (
                <section className="space-y-3">
                    <div className="flex items-center gap-2">
                        <span className="relative flex h-2 w-2">
                            <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-blue-400 opacity-75"></span>
                            <span className="relative inline-flex rounded-full h-2 w-2 bg-blue-500"></span>
                        </span>
                         <h3 className="text-xs font-black text-gray-400 uppercase tracking-widest">
                          Active Automation Schedules
                         </h3>
                    </div>

                    <div className="flex gap-4 overflow-x-auto pb-2 scrollbar-hide">
                        {activeSubscriptions.map((subscription) => (
                            <div
                                key={subscription.riskSubscriptionId}
                                className="flex-shrink-0 w-64 bg-slate-900 text-white p-4 rounded-xl shadow-sm border border-slate-800"
                            >
                                <p className="text-[10px] font-bold text-slate-500 uppercase truncate mb-1">
                                    {subscription.projectName}
                                </p>
                                <div className="flex justify-between items-end">
                                    <div>
                                        <p className="text-[10px] text-slate-400">Next Run</p>
                                        <p className="text-xs font-bold text-blue-400">
                                            {subscription.nextRun}
                                        </p>
                                    </div>
                                </div>
                                <button onClick={()=>handleCancelSubscription(subscription)}  className="mt-2 px-2 py-1 bg-blue-500 text-white text-xs font-semibold rounded-md hover:bg-blue-600 transition-colors">
                                    Cancel Subscription
                                </button>
                            </div>
                        ))}
                        
                    </div>
                </section>
            ):(
             <div>
              <p className="text-sm text-gray-500 mt-1">
              No automated runs schedule <span className="mx-1 text-gray-300">•</span> Schedule new run for fresh data
              </p>
            </div>
            )}
        
            

            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div className="bg-white p-6 rounded-xl border border-gray-100 shadow-sm">
                    <p className="text-xs font-bold text-gray-400 uppercase tracking-wider">Total Projects</p>
                    <p className="text-3xl font-black text-gray-900">{totalProjects}</p>
                </div>
                <div className="bg-white p-6 rounded-xl border border-gray-100 shadow-sm">
                    <p className="text-xs font-bold text-red-400 uppercase tracking-wider">Attention Required</p>
                    <p className="text-3xl font-black text-red-600">{criticalProjects}</p>
                </div>
                <div className="bg-white p-6 rounded-xl border border-gray-100 shadow-sm border-l-4 border-l-blue-500">
                    <p className="text-xs font-bold text-blue-400 uppercase tracking-wider">Active Subscriptions</p>
                    <p className="text-3xl font-black text-gray-900">{activeSubscriptions.length}</p>
                </div>
            </div>


            <div className="grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 gap-6">
                {projects?.map((project) => (
                    <Link
                        key={project.projectId}
                        href={`/dashboard/showprojectmetrics/${project.projectId}`}
                        className="group bg-white p-6 rounded-2xl border border-gray-100 shadow-sm hover:shadow-xl hover:border-blue-200 transition-all duration-300 transform hover:-translate-y-1"
                    >
                        <div className="flex justify-between items-start mb-4">
                            <h3 className="font-bold text-lg text-gray-800 group-hover:text-blue-600 transition-colors truncate pr-4">
                                {project.projectName}
                            </h3>
                            <span className={`text-[10px] font-black px-2 py-1 rounded-md ${project.metrics.healthPercentage > 70 ? 'bg-green-50 text-green-600' : 'bg-amber-50 text-amber-600'
                                }`}>
                                {project.metrics.healthPercentage}%
                            </span>
                        </div>

                        <div className="space-y-4">
                            <div className="flex justify-between text-xs font-bold">
                                <span className="text-gray-400 uppercase tracking-tighter">Critical Risks</span>
                                <span className="text-red-500 bg-red-50 px-2 rounded-full">{project.metrics.critical}</span>
                            </div>

                            <div className="w-full bg-gray-100 h-2 rounded-full overflow-hidden">
                                <div
                                    className={`h-full transition-all duration-1000 ${project.metrics.healthPercentage > 70 ? 'bg-blue-500' : 'bg-amber-500'
                                        }`}
                                    style={{ width: `${project.metrics.healthPercentage}%` }}
                                />
                            </div>
                        </div>
                    </Link>
                ))}
            </div>
        </div>
    );
}