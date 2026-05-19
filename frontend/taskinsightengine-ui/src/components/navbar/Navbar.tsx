'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import '@/app/globals.css'
import { Roles } from '@/types/constants';
import { useCurrentUser, useLogout } from '@/features/auth/hooks/useAuth';

export function Navbar() {
    const pathname = usePathname();
    const { mutate: logout, isPending } = useLogout();

    const user = useCurrentUser();

    const { fullName, role, email: email } = user;


    const isActive = (path: string) => pathname === path ? 'text-blue-600 font-semibold' : 'text-gray-600 hover:text-blue-500';

    return (
        <nav className="fixed top-0 left-0 right-0 h-16 bg-white border-b border-gray-200 z-50">
            <div className="max-w-7xl mx-auto px-4 h-full flex items-center justify-between">

                <div className="flex items-center gap-8">
                    <Link href="/dashboard" className="text-xl font-bold tracking-tight text-black dark:text-black">
                        Task<span className="text-blue-600">Engine</span>
                    </Link>

                    <div className="hidden md:flex items-center gap-6 text-sm">
                        <Link href="/dashboard/createtask" className={`transition-colors ${isActive('/dashboard/createtask')}`}>
                            Add Task
                        </Link>
                        {role === Roles.ProjectManager && (
                            <Link href="/dashboard/createproject" className={`transition-colors ${isActive('/dashboard/createproject')}`}>
                                Add Project
                            </Link>
                        )}
                        <Link href="/dashboard/showprojects" className={`transition-colors ${isActive('/dashboard/showprojects')}`}>
                            Projects
                        </Link>
                         {role === Roles.ProjectManager && (
                            <Link href="/dashboard/showprojectmetrics" className={`transition-colors ${isActive('/dashboard/createproject')}`}>
                                Manage Projects
                            </Link>
                        )}
                    </div>
                </div>


                <div className="flex items-center gap-6">
                    {role === Roles.ProjectManager && (
                        <Link
                            href="/dashboard/risksettings"
                            className={`flex items-center gap-2 px-4 py-1.5 rounded-full transition-all border shadow-sm
                            ${pathname === '/dashboard/risksettings'
                                    ? 'text-blue-600 bg-blue-50 border-blue-200'
                                    : 'text-gray-500 hover:text-blue-600 hover:bg-gray-50 border-gray-100'
                                }`}
                            title="Risk Settings"
                        >
                            <svg className="w-4 h-4 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                            </svg>

                            <span className="text-xs font-semibold whitespace-nowrap">
                                Risk Settings
                            </span>
                        </Link>
                    )}
                    <div className="hidden lg:flex flex-col text-right border-r pr-4 border-gray-200">
                        <p className="text-sm font-semibold text-gray-800 leading-tight">
                            {fullName}
                        </p>
                        <p className="text-xs text-gray-500 font-medium italic">
                            {role}
                        </p>
                        <p className="text-[10px] text-gray-400 font-mono">
                            {email}
                        </p>
                    </div>

                    <button
                        onClick={() => logout()}
                        disabled={isPending}
                        className="flex items-center justify-center px-4 py-2 text-sm font-medium text-white bg-red-500 rounded-lg hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed transition-all shadow-sm"
                    >
                        {isPending ? (
                            <span className="flex items-center gap-2">
                                <svg className="animate-spin h-4 w-4 text-white" viewBox="0 0 24 24">
                                    <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" fill="none" />
                                    <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z" />
                                </svg>
                                logging out...
                            </span>
                        ) : (
                            'Logout'
                        )}
                    </button>
                </div>
            </div>
        </nav>
    );
}