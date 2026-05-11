"use client";

import { useForm } from "react-hook-form";
import { useAddProjectMembers, useCreateProject } from "../hooks/useProjects";
import { useGetAllUsers } from "@/features/auth/hooks/useAuth";
import { Priority } from "../../../types/constants";

type CreateProjectForm = {
    name: string;
    description: string;
    priority: string;
    startDate: string;
    closeDate?: string;
    userIds: number[];
};

export default function CreateProjectForm() {
    const {
        register,
        reset,
        handleSubmit,
        formState: { errors }
    } = useForm<CreateProjectForm>();

    const { mutate: createProject, isPending } = useCreateProject();
    const { mutate: addMembers } = useAddProjectMembers();

    const { data: usersData, isLoading } = useGetAllUsers();

    const onSubmit = (data: CreateProjectForm) => {
        const payload = {
            ...data,
            startDate: new Date(data.startDate).toISOString(),
            closeDate: data.closeDate
                ? new Date(data.closeDate).toISOString()
                : undefined,
            memberIds: data.userIds?.map(Number),
            createdAt: new Date(),
            updatedAt: new Date()
        };

        createProject(payload, {
            onSuccess: (res) => {
                const projectId = res.projectId;

                if (payload.memberIds?.length) {
                    addMembers({
                        projectId,
                        userIds: payload.memberIds,
                        createdAt: new Date(),
                        updatedAt: new Date(),
                    });
                }
                reset();
            }

        });
    };

    return (
        <div className="flex justify-center items-start min-h-screen bg-slate-100 py-12">
            <div className="bg-white border border-slate-200 rounded-lg w-[700px] shadow-sm">

                {/* Header */}
                <div className="border-b border-slate-200 px-8 py-5">
                    <h2 className="text-lg font-semibold text-slate-800">
                        Create Project
                    </h2>
                    <p className="text-sm text-slate-500 mt-1">
                        Add a new project to your workspace
                    </p>
                </div>

                <form onSubmit={handleSubmit(onSubmit)} className="px-8 py-6 space-y-6">

                    {/* Name */}
                    <div>
                        <label className="block text-sm font-medium text-slate-700 mb-1">
                            Project Name
                        </label>
                        <input
                            className="w-full border border-slate-300 rounded-md p-2.5 focus:ring-2 focus:ring-sky-500"
                            {...register("name", { required: "Project name is required" })}
                        />
                        {errors.name && (
                            <p className="text-red-500 text-sm">{errors.name.message}</p>
                        )}
                    </div>

                    {/* Description */}
                    <div>
                        <label className="block text-sm font-medium text-slate-700 mb-1">
                            Description
                        </label>
                        <textarea
                            className="w-full border border-slate-300 rounded-md p-2.5"
                            {...register("description")}
                        />
                    </div>

                    {/* Priority */}
                    <div>
                        <label className="block text-sm font-medium text-slate-700 mb-1">
                            Priority
                        </label>
                        <select
                            {...register("priority", { required: "Priority is required" })}
                            className="w-full border border-slate-300 rounded-md p-2.5"
                        >
                            <option value="">Select Priority</option>
                            <option value={Priority.Low}>Low</option>
                            <option value={Priority.Medium}>Medium</option>
                            <option value={Priority.High}>High</option>
                        </select>
                    </div>

                    {/* Dates Section */}
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                        {/* Start Date */}
                        <div className="flex flex-col">
                            <label className="text-sm font-medium text-slate-700 mb-1.5">
                                Start Date
                            </label>
                            <input
                                type="date"
                                {...register("startDate", { required: "Start date is required" })}
                                className="w-full border border-slate-300 rounded-md p-2 text-sm text-slate-600 focus:outline-none focus:ring-2 focus:ring-sky-500 focus:border-sky-500 transition-all"
                            />
                            {errors.startDate && (
                                <p className="text-red-500 text-xs mt-1">Start date is required</p>
                            )}
                        </div>

                        {/* Close Date */}
                        <div className="flex flex-col">
                            <label className="text-sm font-medium text-slate-700 mb-1.5">
                                Closing Date
                            </label>
                            <input
                                type="date"
                                {...register("closeDate")}
                                className="w-full border border-slate-300 rounded-md p-2 text-sm text-slate-600 focus:outline-none focus:ring-2 focus:ring-sky-500 focus:border-sky-500 transition-all"
                            />
                            {errors.closeDate && (
                                <p className="text-red-500 text-xs mt-1">Closing Date is required</p>
                            )}
                        </div>
                    </div>

                    {/* Members */}
                    <div>
                        <label className="block text-sm font-medium text-slate-700 mb-2">
                            Assign Members
                        </label>

                        <div className="border rounded-md p-2 max-h-40 overflow-y-auto space-y-2">

                            {isLoading && <p>Loading users...</p>}

                            {usersData?.users.map((user) => (
                                <label
                                    key={user.userId}
                                    className="flex justify-between items-center p-2 hover:bg-slate-50 rounded"
                                >
                                    <div>
                                        <p className="text-sm font-medium">{user.userName}</p>
                                        <p className="text-xs text-gray-500">
                                            {user.email} • {user.role}
                                        </p>
                                    </div>

                                    <input
                                        type="checkbox"
                                        value={user.userId}
                                        {...register("userIds")}
                                    />
                                </label>
                            ))}

                        </div>
                    </div>

                    {/* Submit */}
                    <div className="flex justify-end">
                        <button
                            type="submit"
                            disabled={isPending}
                            className="bg-sky-600 text-white px-5 py-2 rounded"
                        >
                            {isPending ? "Creating..." : "Create Project"}
                        </button>
                    </div>

                </form>
            </div>
        </div>
    );
}