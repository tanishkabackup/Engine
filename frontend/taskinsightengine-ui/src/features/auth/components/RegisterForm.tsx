"use client";

import { useForm } from "react-hook-form";
import { useRegister } from "../hooks/useAuth";
import { Roles } from "../../../types/constants";

type RegisterFormData = {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  role: string;
};

export default function RegisterForm() {
  const { mutate, isPending } = useRegister();

  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm<RegisterFormData>();

  const onSubmit = (data: RegisterFormData) => {
    mutate(data);
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-slate-100">

      <div className="bg-white w-[420px] rounded-xl shadow-lg p-8">

        <h2 className="text-2xl font-semibold text-gray-800 text-center mb-6">
          Create Account
        </h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">

          {/* Full Name */}
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Full Name
            </label>
            <input
              className="w-full border border-gray-300 p-2.5 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              placeholder="John Doe"
              {...register("fullName", { required: "Full name is required" })}
            />
            {errors.fullName && (
              <p className="text-red-500 text-xs mt-1">
                {errors.fullName.message}
              </p>
            )}
          </div>

          {/* Email */}
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Email
            </label>
            <input
              className="w-full border border-gray-300 p-2.5 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              placeholder="john@email.com"
              {...register("email", { required: "Email is required" })}
            />
            {errors.email && (
              <p className="text-red-500 text-xs mt-1">
                {errors.email.message}
              </p>
            )}
          </div>

          {/* Password */}
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Password
            </label>
            <input
              type="password"
              className="w-full border border-gray-300 p-2.5 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              {...register("password", { required: "Password is required" })}
            />
            {errors.password && (
              <p className="text-red-500 text-xs mt-1">
                {errors.password.message}
              </p>
            )}
          </div>

          {/* Confirm Password */}
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Confirm Password
            </label>
            <input
              type="password"
              className="w-full border border-gray-300 p-2.5 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              {...register("confirmPassword", {
                required: "Please confirm your password",
                validate: (value, formValues) =>
                  value === formValues.password || "Passwords do not match"
              })}
            />
            {errors.confirmPassword && (
              <p className="text-red-500 text-xs mt-1">
                {errors.confirmPassword.message}
              </p>
            )}
          </div>

          {/* Role */}
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Role
            </label>
            <select
              {...register("role", { required: "Role is required" })}
              className="w-full border border-gray-300 p-2.5 rounded-md bg-white focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
            >
              <option value="">Select Role</option>
              <option value={Roles.Developer}>Developer</option>
              <option value={Roles.ProjectManager}>Project Manager</option>
            </select>

            {errors.role && (
              <p className="text-red-500 text-xs mt-1">
                {errors.role.message}
              </p>
            )}
          </div>

          {/* Button */}
          <button
            type="submit"
            disabled={isPending}
            onClick={() => (window.location.href = '/account/login')}
            className="w-full bg-indigo-600 text-white font-medium py-2.5 rounded-md hover:bg-indigo-700 transition duration-200 shadow-sm"
          >
            {isPending ? "Registering..." : "Create Account"}
          </button>

        </form>

      </div>

    </div>
  );
}