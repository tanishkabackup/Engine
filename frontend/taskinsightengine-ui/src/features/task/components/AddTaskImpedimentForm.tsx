"use client";

import { useCurrentUser } from "@/features/auth/hooks/useAuth";
import { useForm } from "react-hook-form";
import { useAddTaskImpediment } from "../hooks/useTasks";
import { AddTaskImpedimentRequest } from "@/types/request";
import { Risk } from "@/types/constants";
import { useQueryClient } from "@tanstack/react-query";


type AddTaskImpedimentFormData = {
    [FIELDS.TITLE]: string;
    [FIELDS.RISK]: string;
    [FIELDS.IS_RESOLVED]: boolean;
    [FIELDS.RISKDESCRIPTION]:string;
}


const FIELDS ={
    TITLE:"title" as const,
    RISK:"risk" as const,
    IS_RESOLVED: "isResolved" as const,
    RISKDESCRIPTION: "riskDescription" as const
}

interface AddTaskImpedimentFormProps {
  taskId: number;
  onClose: () => void;
}
export default function AddTaskImpedimentForm({ taskId , onClose }: AddTaskImpedimentFormProps){
    const queryClient = useQueryClient();
      const { register, handleSubmit, reset } =
        useForm<AddTaskImpedimentFormData>({
            defaultValues:{
                [FIELDS.IS_RESOLVED]:false,
                [FIELDS.RISK]:"Low"
            }
        });

        const currentUser = useCurrentUser();
        console.log("email",currentUser.email);


        const { mutate: createTaskImpediment , isPending} = useAddTaskImpediment();

        const onSubmit = (data: AddTaskImpedimentFormData) => {
            const taskImpedimentData : AddTaskImpedimentRequest =
            {
                title: data[FIELDS.TITLE],
                riskStatus: data[FIELDS.RISK],
                isResolved: data[FIELDS.IS_RESOLVED],
                resolvedBy: data[FIELDS.IS_RESOLVED] ? currentUser?.email : "",
                riskDescription : data[FIELDS.RISKDESCRIPTION],
                taskId: taskId,
                createdBy : currentUser.email
            }

            createTaskImpediment(taskImpedimentData,{
                onSuccess:()=>{
                    queryClient.invalidateQueries({ queryKey: ["taskImpediments", taskId] });
                    
                    reset();
                    onClose();
                }
            });


            reset();
        }

        return (
        <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-5 p-4 bg-white rounded-md shadow-sm border">
            <h2 className="text-xl font-bold text-gray-800">New Impediment</h2>

            <div className="flex flex-col gap-2">
                <label htmlFor={FIELDS.TITLE} className="text-sm font-medium text-gray-700">
                    Describe the task blocker 
                </label>
                <input 
                    id={FIELDS.TITLE}
                    type="text"
                    {...register(FIELDS.TITLE, { required: true })} 
                    placeholder="Describe the impediment..."
                    className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 outline-none transition-all"
                />
            </div>

            
            <div className="flex flex-col gap-2">
                <label htmlFor={FIELDS.RISK} className="text-sm font-medium text-gray-700">
                    Severity Level
                </label>
                <select 
                    id={FIELDS.RISK}
                    {...register(FIELDS.RISK)} 
                    className="w-full px-3 py-2 border rounded-md bg-white focus:ring-2 focus:ring-blue-500 outline-none"
                >
                    <option value={Risk.Low}>Low</option>
                    <option value={Risk.Medium}>Medium</option>
                    <option value={Risk.High}>High</option>
                    <option value={Risk.Critical}>Critical</option>
                </select>
            </div>

            <div className="flex flex-col gap-2">
    <label 
        htmlFor={FIELDS.RISKDESCRIPTION} 
        className="text-sm font-semibold text-gray-700"
    >
        Description
    </label>
    <textarea
        id={FIELDS.RISKDESCRIPTION}
        {...register(FIELDS.RISKDESCRIPTION, { 
            required: "Please provide a description of the impediment" 
        })}
        placeholder="Enter details about the blocker..."
        rows={4}
        className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent resize-none"
    />
</div>

            
            <div className="flex items-center gap-3 select-none">
                <input 
                    type="checkbox" 
                    id={FIELDS.IS_RESOLVED}
                    {...register(FIELDS.IS_RESOLVED)} 
                    className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                />
                <label htmlFor={FIELDS.IS_RESOLVED} className="text-sm text-gray-600 cursor-pointer">
                    This issue is already resolved
                </label>
            </div>

           
            <div className="flex items-center justify-end gap-3 mt-2">
                <button 
                    type="button" 
                    onClick={onClose}
                    className="px-4 py-2 text-sm font-medium text-gray-600 hover:bg-gray-100 rounded-md transition-colors"
                >
                    Cancel
                </button>
                <button 
                    type="submit" 
                    disabled={isPending}
                    className="px-4 py-2 text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 rounded-md disabled:bg-blue-300 disabled:cursor-not-allowed transition-all shadow-sm"
                >
                    {isPending ? "Creating..." : "Create Impediment"}
                </button>
            </div>
        </form>
    );
}