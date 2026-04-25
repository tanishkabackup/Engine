import '@/app/globals.css'
import { ReactNode } from 'react';
export default function AuthLayout({ children }: { children: ReactNode }) {
  return (
    <div className="min-h-screen flex items-center justify-center bg-blue-50">
      <div className="w-full max-w-md p-6 bg-white shadow-lg rounded-2xl">
        {children}
      </div>
    </div>
  );
}