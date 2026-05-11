"use client";

import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useRef, useEffect } from "react";
import { useForm } from "react-hook-form";
import { useMutation } from "@tanstack/react-query";
import { useSignalR } from "@/features/task/hooks/useSignalR"
import { GetTaskImpedimentCommentsRequest } from "@/types/request";
import { useGetTaskImpedimentComments } from "../hooks/useTasks";

type FormData = {
    comment: string;
}

interface Props {
    impedimentId: number;
}

export default function ImpedimentCommentForm({ impedimentId }: Props) {
    const currentUser = useCurrentUser();
    const scrollRef = useRef<HTMLUListElement>(null);
    const { register, handleSubmit, reset } = useForm<FormData>({
        defaultValues: {
            comment: ""
        }
    });

    const base = process.env.NEXT_PUBLIC_API_HUB!;

    const request: GetTaskImpedimentCommentsRequest = {
        taskImpedimentId: impedimentId
    };

    const { data: history = [], isLoading } = useGetTaskImpedimentComments(request);

    const { isConnected, sendComment } = useSignalR({ base, impedimentId });

    useEffect(() => {
        if (scrollRef.current) {
            scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
        }
    }, [history.length]);

    const { mutate, isPending } = useMutation({
        mutationFn: sendComment,
        onSuccess: () => {
            reset();
        },
    });

    const onSubmit = ({ comment }: FormData) => {
        const trimmedComment = comment?.trim();

        if (!trimmedComment) {
            return;
        }

        mutate({
            taskImpedimentId: impedimentId,
            message: trimmedComment
        });
    }

    return (
        <section className="mt-4 bg-gray-50 p-4 rounded-xl border border-gray-200">
            <header className="flex justify-between items-center mb-3">
                <h3 className="text-[10px] font-bold text-gray-500 uppercase tracking-widest">Discussion</h3>
                {!isConnected && (
                    <span className="text-[10px] text-orange-500 animate-pulse">Connecting...</span>
                )}
            </header>

            <ul ref={scrollRef} className="space-y-4 mb-4 max-h-80 overflow-y-auto pr-2">
                {isLoading ? (
                    <li className="text-xs text-gray-400">Loading history...</li>
                ) : history.length === 0 ? (
                    <li className="text-xs text-gray-400 text-center py-4">
                        No comments yet.
                    </li>
                ) : (
                    history.map((member, i) => {
                        return (
                            <li
                                key={member.commentId ?? i}
                                className="flex gap-3 border-b border-gray-200 pb-3 last:border-none"
                            >
                                <div className="w-8 h-8 flex items-center justify-center rounded-full bg-blue-100 text-blue-600 text-xs font-bold shrink-0">
                                    {member.fullName.charAt(0).toUpperCase()}
                                </div>

                                <div className="flex-1">
                                    <div className="flex items-center gap-2 mb-1">
                                        <span
                                            className={`text-sm font-semibold ${member.email === currentUser?.email
                                                    ? "text-blue-600"
                                                    : "text-gray-800"
                                                }`}
                                        >
                                            {member.fullName}
                                        </span>

                                        <span className="text-xs text-gray-400">
                                            {new Date(member.createdAt).toLocaleString([], {
                                                hour: "2-digit",
                                                minute: "2-digit",
                                                second: undefined
                                            })}
                                        </span>
                                    </div>

                                    <p className="text-sm text-gray-700 whitespace-pre-wrap">
                                        {member.message}
                                    </p>
                                </div>
                            </li>);
                    })
                )}
            </ul>

            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-2 pt-3 border-t">
                <textarea
                    key={isPending ? "sending" : "idle"}
                    {...register("comment", { required: true })}
                    placeholder="Add a comment..."
                    rows={2}
                    className="w-full p-3 text-sm rounded-lg border border-gray-300 focus:ring-2 focus:ring-blue-500 outline-none resize-none"
                    disabled={!isConnected || isPending}
                />

                <div className="flex justify-end">
                    <button
                        type="submit"
                        disabled={!isConnected || isPending}
                        className="px-4 py-2 bg-blue-600 text-white text-xs font-bold rounded-lg hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        {isPending ? "Posting..." : "Comment"}
                    </button>
                </div>
            </form>
        </section>
    );
}