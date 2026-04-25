"use client";

import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { setupSignalRConnection } from "@/lib/signalR";
import { ImpedimentCommentDto, SendImpedimentCommentRequest } from "@/types/request";
import { HubConnection } from "@microsoft/signalr";

export default function ImpedimentCommentForm({ impedimentId }) {
  type FormData = {
    comment: string;
  };

  const connectionRef = useRef<HubConnection | null>(null);
  const [messages, setMessages] = useState<ImpedimentCommentDto[]>([]);

  const { register, handleSubmit, reset } = useForm<FormData>();
  const currentUser = useCurrentUser();

  const base = process.env.NEXT_PUBLIC_API_HUB!;

  const onSubmit = async (data: FormData) => {
    const conn = connectionRef.current;

    if (conn && data.comment.trim()) {
      try {
        const request: SendImpedimentCommentRequest = {
          taskImpedimentId: impedimentId,
          message: data.comment,
        };

        await conn.invoke("SendComment", request);
        reset();
      } catch (err) {
        console.error("Send failed:", err);
      }
    }
  };

  useEffect(() => {
    const conn = setupSignalRConnection(base);
    connectionRef.current = conn;

    let isMounted = true;

    const startConnection = async () => {
      try {
        if (conn.state === "Disconnected") {
          await conn.start();
          console.log("SignalR connected");

          await conn.invoke("JoinImpedimentGroup", impedimentId);

          conn.on("ReceiveComment", (comment: ImpedimentCommentDto) => {
            if (!isMounted) return;
            setMessages((prev) => [...prev, comment]);
          });
        }
      } catch (err) {
        console.error("SignalR Connection Error:", err);
      }
    };

    startConnection();

    return () => {
      isMounted = false;

      conn.off("ReceiveComment"); 
      conn.stop(); 
    };
  }, [impedimentId, base]);

  return (
    <div className="mt-4 bg-gray-50 p-4 rounded-xl border border-gray-200">
      <div className="space-y-3 mb-4 max-h-60 overflow-y-auto pr-2">
        {messages.map((m, i) => (
          <div
            key={m.commentId || i}
            className={`p-2 rounded-lg border shadow-sm ${
              m.fullName === currentUser?.fullName
                ? "bg-blue-50 text-right"
                : "bg-white"
            }`}
          >
            <span className="text-xs font-bold text-blue-600 block">
              {m.fullName}
            </span>

            <span className="text-sm text-gray-700">
              {m.message}
            </span>

            <div className="text-[10px] text-gray-400">
              {new Date(m.createdAt).toLocaleTimeString()}
            </div>
          </div>
        ))}
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="flex gap-2">
        <textarea
          {...register("comment", { required: true })}
          placeholder="Start typing..."
          rows={1}
          className="flex-1 p-2 text-sm rounded-lg border border-gray-300 focus:ring-2 focus:ring-blue-500 outline-none resize-none"
        />

        <button
          type="submit"
          className="px-4 py-2 bg-blue-600 text-white text-xs font-bold rounded-lg hover:bg-blue-700 transition-all"
        >
          Send
        </button>
      </form>
    </div>
  );
}