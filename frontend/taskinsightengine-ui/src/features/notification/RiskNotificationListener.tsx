"use client";

import { setupSignalRConnection } from "@/lib/signalR";
import { BriefingSnapshot } from "@/types/request";
import { useEffect } from "react";
import {toast} from "sonner";


export default function RiskNotificationListener()
{
    useEffect( ()=>{
        const connection = setupSignalRConnection(process.env.NEXT_PUBLIC_RISK_HUB!);

        const startConnection = async()=>{
            try{
                await connection.start();
               
                connection.on("ReceiveDailyBriefing",(snapshot: BriefingSnapshot,projectName:string)=>{
                      const attentionCount = snapshot.needsAttention?.length || 0;
                      const riskCount = snapshot.slientRisk?.length || 0;

                      toast.warning("Daily Briefing Update",{
                        description: `Project:${projectName},has ${attentionCount} tasks that need attention and ${riskCount} silent risks.`,
                        duration: 8000,
                        action:{
                            label: "View Briefing",
                            onClick: () => (window.location.href = "/dashboard/briefing"),
                        },
                      });
                });
            }
            catch(error)
            {
                console.error("Error starting SignalR connection:",error);
            }
        };

        startConnection();

        return() =>{
            connection.stop();
        };
    },[]);

    return null;
}