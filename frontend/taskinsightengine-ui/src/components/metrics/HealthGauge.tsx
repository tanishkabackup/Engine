import { ProjectMetricsDto } from "@/types/response";


export const HealthGauge = ({ percentage, projectMetrics }: {
    percentage: number;
    projectMetrics: ProjectMetricsDto
}) => {
    const { metrics } = projectMetrics;

    return (
        <div className="flex items-center gap-8 bg-white p-4 rounded-xl border border-gray-100 shadow-sm">
            {/* Circle Gauge */}
            <div className="relative h-24 w-24 flex items-center justify-center flex-shrink-0">
                <svg className="h-full w-full transform -rotate-90">
                    <circle cx="50%" cy="50%" r="40%" fill="none" stroke="#f3f4f6" strokeWidth="8" />
                    <circle
                        cx="50%" cy="50%" r="40%" fill="none" strokeWidth="8"
                        stroke={percentage > 70 ? "#10b981" : "#f59e0b"}
                        strokeDasharray="251.2"
                        strokeDashoffset={251.2 - (251.2 * percentage) / 100}
                        strokeLinecap="round"
                        className="transition-all duration-700 ease-in-out"
                    />
                </svg>
                <div className="absolute flex flex-col items-center">
                    <span className="text-xl font-black leading-none">{percentage}%</span>
                    <span className="text-[10px] text-gray-400 uppercase font-bold">Health</span>
                </div>
            </div>

            <div className="grid grid-cols-2 gap-x-6 gap-y-2 border-l pl-8 border-gray-100">
                <CategoryStat label="Critical" count={metrics.critical} color="bg-red-500" />
                <CategoryStat label="Warning" count={metrics.warning} color="bg-amber-500" />
                <CategoryStat label="Recovering" count={metrics.recovering} color="bg-blue-500" />
                <CategoryStat label="Healthy" count={metrics.healthy} color="bg-green-500" />
            </div>
        </div>
    );
};

const CategoryStat = ({ label, count, color }: { label: string; count: number; color: string }) => (
    <div className="flex items-center gap-2">
        <div className={`h-2.5 w-2.5 rounded-full ${color}`} />
        <div className="flex flex-col">
            <span className="text-xs text-gray-500 font-medium leading-none">{label}</span>
            <span className="text-sm font-bold text-gray-800">{count}</span>
        </div>
    </div>
);