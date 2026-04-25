'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { useLogout,useCurrentUser } from '../features/auth/hooks/useAuth';
import '@/app/globals.css'

export function Navbar() {
  const pathname = usePathname();
  const { mutate: logout, isPending } = useLogout();

  const user = useCurrentUser();

  if (!user) {
    return null;
  }
  const { fullName, role, email: email } = user;

 
  const isActive = (path: string) => pathname === path ? 'text-blue-600 font-semibold' : 'text-gray-600 hover:text-blue-500';

  return (
    <nav className="fixed top-0 left-0 right-0 h-16 bg-white border-b border-gray-200 z-50">
      <div className="max-w-7xl mx-auto px-4 h-full flex items-center justify-between">
        
        <div className="flex items-center gap-8">
        <Link href="/dashboard" className="text-xl font-bold tracking-tight text-gray-900">
         Task<span className="text-blue-600">Engine</span>
        </Link>
          
          <div className="hidden md:flex items-center gap-6 text-sm">
            <Link href="/dashboard/createtask" className={`transition-colors ${isActive('/createtask')}`}>
              Add Task
            </Link>
            <Link href="/dashboard/createproject" className={`transition-colors ${isActive('/createproject')}`}>
              Add Project
            </Link>
              <Link href="/dashboard/showprojects" className={`transition-colors ${isActive('/showprojects')}`}>
               Projects
            </Link>
          </div>
        </div>

       
        <div className="flex items-center gap-6">
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