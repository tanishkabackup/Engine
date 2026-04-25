
import { Navbar } from "../../_components/Navbar"; 
import '@/app/globals.css'

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <section>
      <Navbar /> 
      <main className="pt-16 min-h-screen bg-gray-50">
        {children}
      </main>
    </section>
  );
}