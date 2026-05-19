import { TeamMemberWorkload } from "@/types/response";

export const WorkLoadTable = ({ workload }: { workload: TeamMemberWorkload[] }) => {
    return (
        <div className="bg-white rounded-xl border border-gray-200 overflow-hidden">
            <table className="w-full text-left text-sm">
                <thead className="bg-gray-50 text-gray-500 font-medium">
                    <tr>
                        <th className="px-6 py-4">Assignee</th>
                        <th className="px-6 py-4 text-center">No of Task Assigned</th>
                        <th className="px-6 py-4 text-right">Health Status</th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-gray-100">
                    {workload.map((work, index) => (
                        <tr key={index} className="hover:bg-gray-50">
                            <td className="px-6 py-4">
                                <div className="flex flex-col gap-0.5">

                                    <div className="text-sm font-bold text-slate-800 tracking-tight">
                                        {work.assigneeName}
                                    </div>


                                    <div className="text-[11px] font-semibold text-slate-400 uppercase tracking-widest">
                                        {work.roleName}
                                    </div>
                                </div>
                            </td>
                            <td className="px-6 py-4 text-center font-mono font-bold">{work.totalItems}</td>
                            <td className="px-6 py-4">
                                <div className="flex justify-end gap-1.5">
                                    <div className={`h-2 w-2 rounded-full ${work.critical > 0 ? 'bg-red-500' : 'bg-gray-200'}`} />
                                    <div className={`h-2 w-2 rounded-full ${work.warning > 0 ? 'bg-amber-400' : 'bg-gray-200'}`} />
                                    <div className="h-2 w-2 rounded-full bg-green-400" />
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}