'use client';

import { useForm } from "react-hook-form";
import { useLogin } from "../hooks/useAuth";
import { LoginRequest } from "../../../types/request";
import Link from "next/link"; 

export default function LoginForm() {
  const { mutate: login, isPending, error, isError } = useLogin();

  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm<LoginRequest>({
    mode: "onBlur" 
  });

  const onSubmit = (data: LoginRequest) => {
    login(data);
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-[#f8fafc]">
      <div className="bg-white w-full max-w-[420px] rounded-2xl shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-slate-100 p-10 transition-all">

        <div className="w-12 h-12 bg-indigo-600 rounded-xl mx-auto mb-6 flex items-center justify-center shadow-lg shadow-indigo-200">
           <span className="text-white font-bold text-xl">T</span>
        </div>

        <h2 className="text-2xl font-bold text-slate-800 text-center mb-2">
          Welcome Back
        </h2>
        <p className="text-slate-500 text-center mb-8 text-sm">
          Enter your details to access your dashboard
        </p>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          
          {/* Email */}
          <div className="space-y-1.5">
            <label className="text-xs font-semibold text-slate-700 uppercase tracking-wider ml-1">
              Email Address
            </label>
            <input
              type="email"
              className={`w-full border p-3 rounded-xl focus:outline-none focus:ring-4 transition-all duration-200 ${
                errors.email 
                  ? "border-red-300 focus:ring-red-100" 
                  : "border-slate-200 focus:ring-indigo-50 focus:border-indigo-500"
              }`}
              placeholder="name@company.com"
              {...register("email", { 
                required: "Email is required",
                pattern: {
                  value: /\S+@\S+\.\S+/,
                  message: "Invalid email format"
                }
              })}
            />
            {errors.email && (
              <p className="text-red-500 text-[11px] font-medium ml-1">
                {errors.email.message}
              </p>
            )}
          </div>

          {/* Password */}
          <div className="space-y-1.5">
            <div className="flex justify-between items-center ml-1">
              <label className="text-xs font-semibold text-slate-700 uppercase tracking-wider">
                Password
              </label>
              <Link href="/forgot-password" className="text-[11px] text-indigo-600 hover:text-indigo-700 font-bold">
                Forgot?
              </Link>
            </div>
            <input
              type="password"
              className={`w-full border p-3 rounded-xl focus:outline-none focus:ring-4 transition-all duration-200 ${
                errors.password 
                  ? "border-red-300 focus:ring-red-100" 
                  : "border-slate-200 focus:ring-indigo-50 focus:border-indigo-500"
              }`}
              placeholder="••••••••"
              {...register("password", { required: "Password is required" })}
            />
            {errors.password && (
              <p className="text-red-500 text-[11px] font-medium ml-1">
                {errors.password.message}
              </p>
            )}
          </div>

          {/* API Error Handling */}
          {isError && (
            <div className="animate-in fade-in slide-in-from-top-1 bg-red-50 border border-red-100 text-red-600 text-xs p-3 rounded-xl text-center font-medium">
               Login failed. Please enter valid username and password.
            </div>
          )}

          {/* Login Button */}
          <button
            type="submit"
            disabled={isPending}
            className="w-full bg-slate-900 text-white font-semibold py-3.5 rounded-xl hover:bg-slate-800 active:scale-[0.98] transition-all duration-200 shadow-md disabled:opacity-70 disabled:cursor-not-allowed flex items-center justify-center gap-2"
          >
            {isPending ? (
              <>
                <div className="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin" />
                <span>Verifying...</span>
              </>
            ) : (
              "Sign In"
            )}
          </button>

          {/* Link to Register */}
          <p className="text-center text-sm text-slate-500 pt-2">
            New here?{" "}
            <Link href="/account/register" className="text-indigo-600 hover:text-indigo-700 font-bold transition-colors">
              Create an account
            </Link>
          </p>

        </form>
      </div>
    </div>
  );
}