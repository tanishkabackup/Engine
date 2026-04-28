import {useEffect, useRef, useState} from "react";
import { setupSignalRConnection } from "@/lib/signalR";
import { SendImpedimentCommentRequest } from "@/types/request";
import { useQueryClient } from "@tanstack/react-query";

interface Options
{
    base:string;
    impedimentId: string | number;
}

export function useSignalR({base,impedimentId}:Options)
{
      
      const[isConnected,setIsConnected] = useState(false);
      const connRef = useRef(setupSignalRConnection(base));
      const queryClient = useQueryClient();

      useEffect( () =>{

        const conn = connRef.current;

        const onComment = () =>
        {
            queryClient.invalidateQueries({
                queryKey:["taskImpedimentComments",impedimentId],
            });
        }

        const start = async() =>
        {
            if(conn.state==="Disconnected") {
                await conn.start();
            }

            await conn.invoke("JoinImpedimentGroup",impedimentId);
            conn.on("ReceiveComment",onComment);
            setIsConnected(true);
        }

        start().catch(()=> setIsConnected(false));

        return () =>{
            conn.off("ReceiveComment",onComment)
            conn.invoke("LeaveImpedimentGroup",impedimentId).catch(()=>{});
            setIsConnected(false);
        };
      
    },[impedimentId]);

    const sendComment = async(req: SendImpedimentCommentRequest)=>
    {
        const conn = connRef.current;

        if (!conn || conn.state !== "Connected") {
        throw new Error("SignalR not connected");
        }

        return await conn.invoke("SendComment", req);
    };

    return {isConnected, sendComment};
}
