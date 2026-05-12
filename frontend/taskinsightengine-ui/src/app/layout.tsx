"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import React from "react";
import '@/app/globals.css'
import { Geist } from "next/font/google";
import { cn } from "@/lib/utils";
import { Toaster } from "sonner";
import RiskNotificationListener from "@/features/notification/RiskNotificationListener";

const geist = Geist({subsets:['latin'],variable:'--font-sans'});

const queryClient = new QueryClient();


export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html className={cn("font-sans", geist.variable)}>
      <body>
        <QueryClientProvider client={queryClient}>
          <Toaster/>
           <RiskNotificationListener/>
          {children}
        </QueryClientProvider>
      </body>
    </html>
  );
}