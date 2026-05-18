"use client";

import { useForm } from "react-hook-form";
import { CreateProjectRiskSubscriptionRequest } from "@/types/request";
import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useGetUserProjects } from "@/features/project/hooks/useGetUserProjects";
import { ProjectDetail } from "@/types/response";
import { useCreateRiskSubscription } from "@/features/project/hooks/useProjects";

const RISKFORM_FIELDS = {
    HOURS: "hours",
    MINUTES: "minutes",
    PROJECT_IDS: "projectIds"

} as const;

type RiskSettingsFormData = {
    [RISKFORM_FIELDS.HOURS]: number;
    [RISKFORM_FIELDS.MINUTES]: number;
    [RISKFORM_FIELDS.PROJECT_IDS]: [];
}

export default function RiskSettingsForm() {
    const { register, reset, handleSubmit } = useForm<RiskSettingsFormData>();

    const { mutate: createRiskSubscription } = useCreateRiskSubscription();

    const { email: currentUserEmail } = useCurrentUser();

    const { isLoading, projects: sortedProjects } = useGetUserProjects(currentUserEmail);

    const onSubmit = (data: RiskSettingsFormData) => {
        const createRiskSubscriptionRequest = {
            hours: Number(data[RISKFORM_FIELDS.HOURS]),
            minutes: data[RISKFORM_FIELDS.MINUTES],
            projectIds: [].concat(data[RISKFORM_FIELDS.PROJECT_IDS] || []).map(Number),
            email: currentUserEmail
        } as CreateProjectRiskSubscriptionRequest
        createRiskSubscription(createRiskSubscriptionRequest, {
            onSuccess: () => {
                reset();
            }
        });
    }

    if (isLoading) {
        return <div className="p-8 text-slate-500 animate-pulse text-center">Loading settings...</div>;
    }

    return (
        <div className="max-w-4xl mx-auto my-10 bg-white border border-slate-200 rounded-xl shadow-sm overflow-hidden">
            <div className="px-6 py-5 border-b border-slate-100 bg-slate-50/50">
                <h2 className="text-xl font-semibold text-slate-800">Risk Monitoring Settings</h2>
                <p className="text-sm text-slate-500 mt-1">Configure your daily summary and project monitoring.</p>
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="p-6 space-y-8">

                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    <div className="pr-4">
                        <h3 className="text-sm font-semibold text-slate-900">Delivery Schedule</h3>
                        <p className="text-xs text-slate-500 mt-1">Set the time for the risk report delivery.</p>
                    </div>
                    <div className="md:col-span-2 flex gap-4">
                        <div className="flex-1">
                            <label className="block text-[10px] font-bold uppercase text-slate-400 mb-1 ml-1">Hours (0-23)</label>
                            <input
                                type="number"
                                {...register(RISKFORM_FIELDS.HOURS, { required: true, min: 0, max: 23 })}
                                className="w-full border border-slate-300 rounded-lg px-4 py-2.5 focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all"
                                placeholder="08"
                            />
                        </div>
                        <div className="flex-1">
                            <label className="block text-[10px] font-bold uppercase text-slate-400 mb-1 ml-1">Minutes (0-59)</label>
                            <input
                                type="number"
                                {...register(RISKFORM_FIELDS.MINUTES, { required: true, min: 0, max: 59 })}
                                className="w-full border border-slate-300 rounded-lg px-4 py-2.5 focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all"
                                placeholder="30"
                            />
                        </div>
                    </div>
                </div>

                <hr className="border-slate-100" />

                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    <div className="pr-4">
                        <h3 className="text-sm font-semibold text-slate-900">Monitored Projects</h3>
                        <p className="text-xs text-slate-500 mt-1">Notifications will only trigger for the projects checked here.</p>
                    </div>
                    <div className="md:col-span-2">
                        <div className="border border-slate-200 rounded-lg divide-y divide-slate-100 max-h-64 overflow-y-auto bg-slate-50/30">
                            {sortedProjects.length > 0 ? (
                                sortedProjects.map((project: ProjectDetail) => (
                                    <label key={project.projectId} className="flex items-center px-4 py-3 hover:bg-white cursor-pointer transition-colors group">
                                        <input
                                            type="checkbox"
                                            value={project.projectId}
                                            {...register(RISKFORM_FIELDS.PROJECT_IDS)}
                                            className="w-4 h-4 text-blue-600 border-slate-300 rounded focus:ring-blue-500"
                                        />
                                        <span className="ml-3 text-sm text-slate-600 group-hover:text-slate-900 transition-colors">
                                            {project.name}
                                        </span>
                                    </label>
                                ))
                            ) : (
                                <div className="p-4 text-sm text-slate-400 italic text-center">No projects available</div>
                            )}
                        </div>
                    </div>
                </div>

                <hr className="border-slate-100" />

                <div className="flex items-center justify-end gap-3 pt-6 border-t border-slate-100">
                    <button
                        type="submit"
                        className="bg-blue-600 hover:bg-blue-700 active:scale-95 text-white px-8 py-2.5 rounded-lg text-sm font-semibold shadow-md shadow-blue-200 transition-all"
                    >
                        Save Changes
                    </button>
                </div>
            </form>
        </div>
    )

}