"use client";

import { Risk } from "@/types/constants";
import { TaskImpedimentDetail } from "@/types/response";
import { useState } from "react";
import ImpedimentCommentForm from "./ImpedimentCommentForm";

export interface ShowTaskImpedimentsProps {
  impediments: TaskImpedimentDetail[];
}

export default function ShowTaskImpediments({ impediments }: ShowTaskImpedimentsProps) {

  const [activeImpedimentId, setActiveImpedimentId] = useState<number | null>(null);
  

  const riskColorMap: Record<string, string> = {
    [Risk.Critical]: "bg-rose-600 text-white border-rose-600 shadow-rose-100",
    [Risk.High]: "bg-orange-500 text-white border-orange-500 shadow-orange-100",
    [Risk.Medium]: "bg-amber-100 text-amber-700 border-amber-200",
    [Risk.Low]: "bg-emerald-50 text-emerald-700 border-emerald-200",
  };

  return (
    <div className="flex flex-col gap-4">
      {impediments?.length > 0 ? (
        impediments
          .filter((imp) =>
            activeImpedimentId
              ? imp.taskImpedimentId === activeImpedimentId
              : true
          )
          .map((imp, index) => {

            const isCritical = imp.riskStatus === Risk.Critical;
            const isBlocking = !imp.isResolved;

            return (
              <div
                key={`${imp.taskImpedimentId}-${index}`}
                className={`relative rounded-xl border ${
                  isBlocking && isCritical
                    ? "bg-red-50/40 border-red-200"
                    : "bg-white border-slate-200"
                }`}
              >
                {/* Left border indicator */}
                {isBlocking && (
                  <div className={`absolute left-0 top-0 bottom-0 w-1 ${
                    isCritical ? "bg-red-600" : "bg-amber-500"
                  }`} />
                )}

                <div className="p-5">

                  {/* HEADER */}
                  <div className="flex justify-between items-start mb-4">
                    <div>
                      <h3 className="text-sm font-bold text-slate-900">
                        {imp.title}
                      </h3>
                      <p className="text-[10px] text-slate-400">
                        ID: {imp.taskImpedimentId}
                      </p>
                    </div>

                    <span className={`px-2 py-1 text-[10px] rounded border ${
                      riskColorMap[imp.riskStatus]
                    }`}>
                      {imp.riskStatus}
                    </span>
                  </div>

                  {/* DESCRIPTION */}
                  <p className="text-xs text-slate-600 mb-6">
                    {imp.riskDescription}
                  </p>

                  {/* FOOTER (HORIZONTAL ONLY) */}
                  <div className="flex justify-between items-end pt-4 border-t border-slate-100">

                    {/* LEFT */}
                    <div className="flex items-center gap-3">
                      <div className="w-8 h-8 rounded-full bg-slate-100 flex items-center justify-center text-[10px] font-bold">
                        {imp.createdBy
                          ? imp.createdBy.charAt(0).toUpperCase()
                          : "U"}
                      </div>

                      <div className="flex flex-col">
                        <span className="text-[10px] text-slate-400 font-bold">
                          Reported By
                        </span>
                        <span className="text-xs font-semibold">
                          {imp.createdBy}
                        </span>
                      </div>
                    </div>

                    {/* RIGHT */}
                    <div className="flex items-center gap-4">

                      <button className={`text-[10px] px-2 py-1 rounded ${
                        imp.isResolved
                          ? "bg-emerald-50 text-emerald-700"
                          : "bg-red-600 text-white"
                      }`}>
                        {imp.isResolved ? "Resolved" : "Action Required"}
                      </button>

                      <button
                        onClick={() =>
                          setActiveImpedimentId(
                            activeImpedimentId === imp.taskImpedimentId
                              ? null
                              : imp.taskImpedimentId
                          )
                        }
                        className="text-blue-600 text-sm hover:underline"
                      >
                        {activeImpedimentId === imp.taskImpedimentId
                          ? "Close Discussion"
                          : "Show Discussion"}
                      </button>

                    </div>
                  </div>

                  
                  {activeImpedimentId === imp.taskImpedimentId && (
                    <div className="mt-6 pt-4 border-t border-gray-200">
                      <div className="p-5 rounded-xl border bg-white shadow-sm">

                        <div className="flex justify-between items-center mb-4">
                          <h2 className="text-sm font-bold text-slate-800">
                            Discussion
                          </h2>

                          <button
                            onClick={() => setActiveImpedimentId(null)}
                            className="text-xs text-gray-500 hover:text-black"
                          >
                            Close ✕
                          </button>
                        </div>

                        <ImpedimentCommentForm
                          impedimentId={imp.taskImpedimentId}
                        />
                      </div>
                    </div>
                  )}

                </div>
              </div>
            );
          })
      ) : (
        <div className="py-12 flex flex-col items-center border border-dashed rounded-2xl">
          <p className="text-sm text-gray-400">No Blockers</p>
        </div>
      )}

    </div>
  );
}