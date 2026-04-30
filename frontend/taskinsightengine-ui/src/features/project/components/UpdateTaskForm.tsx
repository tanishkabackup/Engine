import { TaskDetail } from "@/types/response";
import { DailyTaskUpdateRequest } from "@/types/request";
import { Status } from "@/types/constants";
import { UseDailyTaskUpdate, useGetDailyTaskUpdates, useGetImpediments } from "@/features/task/hooks/useCreateTask";
import { useForm } from "react-hook-form";
import { useQueryClient } from "@tanstack/react-query";
import { TaskActionHeader } from "@/features/task/components/TaskActionHeader";
import { useState } from "react";
import ShowTaskImpediments from "@/features/task/components/ShowTaskImpedimentForm";

interface UpdateTaskFormProps {
    task: TaskDetail;
    memberId: number;
    onBack: () => void;
}

type DailyTaskUpdateFields = {
    status: string;
    updatedEta: string;
    comment: string;
    effortHours?: number;
};


const STATUS_OPTIONS = [Status.New, Status.InProgress, Status.OnHold, Status.InReview, Status.Completed];

export default function UpdateTaskForm({ task, memberId, onBack }: UpdateTaskFormProps) {

    const { mutate } = UseDailyTaskUpdate();
    const [viewImpediments, setImpedimentsView] = useState<boolean>(false);

    const queryClient = useQueryClient();


    const { register, handleSubmit, reset, formState: { isSubmitting } } =
        useForm<DailyTaskUpdateFields>({
            defaultValues: {
                status: STATUS_OPTIONS[0],
                updatedEta: new Date().toISOString().split("T")[0],
                comment: "",
                effortHours: 0,
            }
        });

    const onSubmit = (data: DailyTaskUpdateFields) => {
        const request: DailyTaskUpdateRequest = {
            taskId: task.taskId,
            status: data.status,
            updatedEta: data.updatedEta ? new Date(data.updatedEta) : undefined,
            comment: data.comment,
            effortHours: data.effortHours,
            projectMemberId: memberId,

        }

        console.log("Submitting Daily Task Update:", request);

        mutate(request, {
            onSuccess: () => {
                queryClient.invalidateQueries({ queryKey: ["taskUpdates", task.taskId] });
                reset();
                alert("Daily update submitted successfully!");
            }
        });
    };



    const { data: taskUpdates } = useGetDailyTaskUpdates({ taskId: task.taskId });

    const { data: taskImpediments } = useGetImpediments({ taskId: task.taskId })


    console.log("Task Data Received:", task);
    const formattedEta = task.expectedEta ? new Date(task.expectedEta).toISOString().split('T')[0] : "";

    const latestUpdate = taskUpdates?.slice().sort((a, b) =>
        new Date(b.lastUpdatedDate).getTime() - new Date(a.lastUpdatedDate).getTime()
    )[0];

    const currentStatus = latestUpdate?.status;
    return (
        <div className="max-w-5xl mx-auto p-8 bg-white rounded-[2.5rem] shadow-sm border border-slate-200">

            {/* 1. TOP BAR: Navigation & Status */}
            <div className="flex flex-wrap items-center justify-between gap-4 mb-8">
                <button onClick={onBack} className="flex items-center gap-2 text-slate-400 hover:text-slate-900 font-bold transition-all">
                    <span className="bg-slate-100 p-2 rounded-xl text-xs">←</span> Back to Project
                </button>

                <div className="flex items-center gap-3">
                    <span className="text-[10px] font-black text-slate-400 uppercase tracking-widest">Status:</span>
                    <select
                        value={currentStatus || STATUS_OPTIONS[0]}
                        disabled
                        className="bg-sky-50 text-sky-700 border-none rounded-full px-4 py-2 font-black text-xs uppercase tracking-wider outline-none cursor-default appearance-none"
                        >
                        {STATUS_OPTIONS.map(s => (
                            <option key={s} value={s}>{s}</option>
                        ))}
                    </select>
                </div>

                <div className="space-y-1">
                    <p className="text-[10px] font-black text-slate-400 uppercase">Hours Assigned</p>
                    <p className="text-sm font-bold text-slate-700">{task.hours}</p>
                </div>

            </div>

            {/* 2. TITLE & DATES SECTION */}
            <div className="mb-10">
                <h2 className="text-4xl font-black text-slate-900 mb-6">{task.title}</h2>

                <div className="flex flex-wrap gap-8 py-6 border-y border-slate-50">
                    <div className="space-y-1">
                        <p className="text-[10px] font-black text-slate-400 uppercase">Expected ETA</p>
                        <input
                            type="date"
                            id="expectedEta"
                            defaultValue={formattedEta}
                            className="text-sm font-bold text-slate-700 bg-transparent border-none p-0 focus:ring-0 cursor-pointer"
                        />
                        <p className="text-[10px] font-black text-slate-400 uppercase">Closing Date</p>
                        <input
                            type="date"
                            id="closingDate"
                            className="text-sm font-bold text-slate-700 bg-transparent border-none p-0 focus:ring-0 cursor-pointer"
                        />
                    </div>
                    <div className="space-y-1">
                        <p className="text-[10px] font-black text-slate-400 uppercase">Opened Date</p>
                        <p className="text-sm font-bold text-slate-700">{new Date(task.createdAt).toLocaleDateString()}</p>
                    </div>
                    <div className="space-y-1">
                        <p className="text-[10px] font-black text-slate-400 uppercase">Last Updated</p>
                        <p className="text-sm font-bold text-slate-700">{new Date(task.updatedAt).toLocaleDateString()}</p>
                    </div>
                </div>
            </div>

            {/* 3. MAIN CONTENT: Description & Assignments */}
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-12 mb-12">
                <div className="lg:col-span-2 space-y-8">
                    <div className="space-y-3">
                        <label className="text-xs font-black text-slate-900 uppercase tracking-widest">Description</label>
                        <textarea
                            defaultValue={task.description}
                            rows={6}
                            className="w-full bg-slate-50 border-none rounded-3xl p-6 text-slate-600 font-medium focus:bg-white focus:ring-2 focus:ring-slate-100 outline-none transition-all resize-none"
                            placeholder="Describe the task details..."
                        />
                    </div>

                    <div>
                        <div>
                            {taskImpediments?.length > 0 ? (
                                <>
                                    {viewImpediments ? (
                                        <div className="space-y-3">
                                            <ShowTaskImpediments impediments={taskImpediments} />
                                            <button
                                                onClick={() => setImpedimentsView(false)}
                                                className="text-slate-400 text-[9px] font-bold hover:text-slate-600 uppercase"
                                            >
                                                ↑ Collapse Details
                                            </button>
                                        </div>
                                    ) : (
                                        <div className="flex items-center justify-between p-2 bg-rose-50 border border-rose-100 rounded-md">
                                            <div className="flex items-center gap-2 truncate">
                                                <span className="flex-none h-2 w-2 rounded-full bg-rose-500"></span>
                                                <div className="flex items-center justify-between w-full gap-4">
                                                    <span className="text-[11px] font-bold text-rose-900 uppercase truncate">
                                                        <span className="opacity-70 mr-1">Task Blocker:</span>
                                                        {taskImpediments[0].title}
                                                    </span>

                                                    <span className="flex-none text-[10px] font-medium text-rose-700/60 uppercase whitespace-nowrap">
                                                        <span className="font-bold">Reported:</span> {new Date(taskImpediments[0].createdAt).toLocaleDateString()}
                                                    </span>
                                                </div>
                                            </div>

                                            <button
                                                onClick={() => setImpedimentsView(true)}
                                                className="flex-none text-sky-600 text-[9px] font-bold hover:underline uppercase ml-4"
                                            >
                                                View All Blockers ({taskImpediments.length}) →
                                            </button>
                                        </div>
                                    )}
                                </>
                            ) : (
                                <div className="py-2.5 px-4 flex items-center justify-between border border-sky-100 rounded-lg bg-gradient-to-r from-sky-50 to-emerald-50/50 shadow-sm">
                                    {/* Left Side: Status & Label */}
                                    <div className="flex items-center gap-3">
                                        {/* Soft Green Signal */}
                                        <div className="relative flex items-center justify-center">
                                            <div className="h-2 w-2 rounded-full bg-emerald-400" />
                                            {/* Static outer ring for depth */}
                                            <div className="absolute h-4 w-4 rounded-full border border-emerald-200/50" />
                                        </div>

                                        <div className="flex flex-col">
                                            <p className="text-[10px] font-black text-sky-900/70 uppercase tracking-[0.15em] leading-none">
                                                No active blockers
                                            </p>
                                            <p className="text-[9px] font-bold text-emerald-700/60 uppercase mt-1 leading-none">
                                                The task currently has no blockers or open queries
                                            </p>
                                        </div>
                                    </div>

                                    <div className="flex items-center gap-2 px-2 py-0.5 rounded bg-white/60 border border-sky-200/50">
                                        <span className="text-[8px] font-black text-sky-400 uppercase tracking-tighter">
                                            Verified {new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                                        </span>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    <div className="space-y-12">
                        <div className="space-y-6">
                            {taskUpdates && taskUpdates.length > 0 ? (
                                taskUpdates.map((update, index) => (
                                    <div
                                        key={index}
                                        className="group bg-slate-100 border-2 border-slate-100 rounded-[2rem] p-6 shadow-sm hover:border-sky-100 transition-all"
                                    >
                                        <div className="flex gap-6">
                                            <a
                                                href={`mailto:${update.Email}`}
                                                title={`Email ${update.projectMemberName}`}
                                                className="group relative flex items-center gap-3 cursor-pointer"
                                            >
                                                {/* The Avatar Box */}
                                                <div className="hidden sm:flex w-12 h-12 rounded-2xl bg-slate-50 items-center justify-center text-slate-400 font-bold text-xs shrink-0 border border-slate-100 group-hover:bg-blue-50 group-hover:text-blue-500 group-hover:border-blue-200 transition-colors">
                                                    {update.projectMemberName ? update.projectMemberName.charAt(0).toUpperCase() : "U"}
                                                </div>

                                                {/* The Name Text */}
                                                <span className="text-slate-600 group-hover:text-blue-600 group-hover:underline transition-all">
                                                    {update.projectMemberName}
                                                </span>
                                            </a>

                                            <div className="flex-1 space-y-4">
                                                {/* Header: Status and Metadata in a single clean row */}
                                                <div className="flex flex-wrap items-center justify-between gap-4">
                                                    <div className="flex items-center gap-3">
                                                        <span className="text-[10px] font-black px-3 py-1.5 bg-sky-50 text-sky-700 rounded-xl uppercase tracking-wider border border-sky-100">
                                                            {update.status}
                                                        </span>
                                                        <span className="text-[10px] font-black text-slate-800 uppercase tracking-tight">
                                                            {update.effortHours} Hours Logged
                                                        </span>
                                                    </div>

                                                    <div className="text-[10px] font-black text-slate-700 uppercase">
                                                        NEW ETA: {update.updatedEta ? new Date(update.updatedEta).toLocaleDateString() : "N/A"}
                                                    </div>
                                                </div>

                                                {/* Content: The Comment Box Style */}
                                                <div className="bg-slate-200/50 rounded-2xl p-5 border border-slate-200">
                                                    <p className="text-sm font-medium text-slate-900 leading-relaxed">
                                                        {update.comment}
                                                    </p>
                                                </div>

                                                {/* Footer: Date Stamp */}
                                                <div className="flex justify-end">
                                                    <span className="text-[9px] font-black text-slate-500 uppercase tracking-[0.15em]">
                                                        LastUpdated : {update.lastUpdatedDate ? new Date(update.lastUpdatedDate).toLocaleDateString() : "N/A"}
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                ))
                            ) : (
                                /* Empty State Box */
                                <div className="bg-slate-50 border-2 border-dashed border-slate-200 rounded-[2rem] py-12 flex flex-col items-center justify-center">
                                    <div className="text-slate-300 mb-2">
                                        <svg className="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                                        </svg>
                                    </div>
                                    <p className="text-[10px] font-black text-slate-400 uppercase tracking-widest">No activity history yet</p>
                                </div>
                            )}
                        </div>


                        <div className="relative py-4">
                            <div className="absolute inset-0 flex items-center"><span className="w-full border-t border-slate-100"></span></div>
                            <div className="relative flex justify-center text-xs uppercase font-black text-slate-300 bg-white px-4">
                                Add New Update
                            </div>
                        </div>
                    </div>




                    {/* DAILYTASKUPDATE*/}
                    <form onSubmit={handleSubmit(onSubmit)} className="mt-8 pt-8 border-t border-slate-100">
                        <label className="text-xs font-black text-slate-900 uppercase tracking-widest mb-6 block">
                            Activity & Comments
                        </label>

                        <div className="flex gap-6">
                            {/* User Avatar */}
                            <div className="hidden sm:flex w-12 h-12 rounded-2xl bg-sky-600 items-center justify-center text-white font-bold text-sm shadow-lg shadow-sky-100 shrink-0">
                                ME
                            </div>

                            <div className="flex-1 space-y-6">
                                {/* Input Grid: Status, ETA, and Hours */}
                                <div className="grid grid-cols-1 md:grid-cols-3 gap-4 p-5 bg-slate-50 rounded-3xl border border-slate-100">

                                    {/* Status Group */}
                                    <div className="space-y-2">
                                        <p className="text-[10px] font-black text-slate-400 uppercase ml-1">Current Status</p>
                                        <select
                                            {...register("status")}
                                            className="w-full bg-white text-sky-700 border border-slate-200 rounded-xl px-4 py-2.5 font-bold text-xs uppercase tracking-wider focus:ring-2 focus:ring-sky-500 outline-none cursor-pointer transition-all shadow-sm"
                                        >
                                            {STATUS_OPTIONS.map(s => <option key={s} value={s}>{s}</option>)}
                                        </select>
                                    </div>

                                    <div className="space-y-2">
                                        <p className="text-[10px] font-black text-slate-400 uppercase ml-1">Effort Hours</p>
                                        <input
                                            type="number"
                                            step="0.5"
                                            {...register("effortHours")}
                                            placeholder="0.0"
                                            className="w-full bg-white text-slate-700 border border-slate-200 rounded-xl px-4 py-2.5 font-bold text-xs focus:ring-2 focus:ring-sky-500 outline-none transition-all shadow-sm"
                                        />
                                    </div>

                                    {/* ETA Group */}
                                    <div className="space-y-2">
                                        <p className="text-[10px] font-black text-slate-400 uppercase ml-1">Revised ETA</p>
                                        <input
                                            type="date"
                                            {...register("updatedEta")}
                                            className="w-full bg-white text-slate-700 border border-slate-200 rounded-xl px-4 py-2.5 font-bold text-xs focus:ring-2 focus:ring-sky-500 outline-none transition-all shadow-sm"
                                        />
                                    </div>

                                </div>

                                {/* Comment Area */}
                                <div className="space-y-3">
                                    <textarea
                                        {...register("comment", { required: true })}
                                        placeholder="Please provide details about the update, any blockers, or relevant information for the team."
                                        rows={4}
                                        className="w-full bg-white border-2 border-slate-100 rounded-2xl p-5 text-sm text-slate-600 focus:border-sky-500 focus:ring-4 focus:ring-sky-50 outline-none transition-all resize-none placeholder:text-slate-300"
                                    />

                                    <button
                                        type="submit"
                                        disabled={isSubmitting}
                                        className="group relative w-full py-4 bg-slate-900 text-white rounded-2xl font-black text-xs tracking-[0.2em] hover:bg-sky-600 shadow-xl shadow-slate-200 transition-all active:scale-[0.99] disabled:opacity-50 flex items-center justify-center gap-3"
                                    >
                                        {isSubmitting ? (
                                            "SYNCING DATA..."
                                        ) : (
                                            <>
                                                SUBMIT DAILY UPDATE
                                                <span className="group-hover:translate-x-1 transition-transform">→</span>
                                            </>
                                        )}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </form>

                    <div className="flex items-center justify-between border-b pb-4">
                        <h2 className="text-xl font-semibold">Edit Task</h2>
                        <TaskActionHeader task={task} />
                    </div>
                </div>

                {/* SIDEBAR: People involved */}
                <div className="space-y-6">
                    <div className="bg-slate-50 p-6 rounded-3xl space-y-6">
                        <div>
                            <p className="text-[10px] font-black text-slate-400 uppercase mb-3">Assigned To</p>
                            <p className="text-xs font-bold text-slate-700 leading-relaxed">{task.allAssignees}</p>
                        </div>
                        <div>
                            <p className="text-[10px] font-black text-slate-400 uppercase mb-3">Assigned By</p>
                            <p className="text-xs font-bold text-slate-700">{task.allAssigners}</p>
                        </div>
                        <div>
                            <p className="text-[10px] font-black text-slate-400 uppercase mb-3">Managers</p>
                            <p className="text-xs font-bold text-slate-700">{task.allManagers}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}