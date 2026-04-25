import { useState } from 'react';
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetTrigger } from  "@/components/ui/sheet";
import AddTaskImpedimentForm from './AddTaskImpedimentForm';

export const TaskActionHeader = ({ task }) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <Sheet open={isOpen} onOpenChange={setIsOpen}>

      <SheetTrigger asChild>
        <button className="flex items-center gap-2 px-4 py-2 bg-red-50 text-red-600 hover:bg-red-100 rounded-md transition-colors">
          <span>Flag Impediment</span>
        </button>
      </SheetTrigger>

      <SheetContent side="right" className="w-[400px] sm:w-[540px]">
        <SheetHeader>
          <SheetTitle>Add Impediment for {task.title}</SheetTitle>
        </SheetHeader>
        
        <div className="mt-6">
          <AddTaskImpedimentForm
            taskId={task.taskId} 
            onClose={() => setIsOpen(false)} 
          />
        </div>
      </SheetContent>
    </Sheet>
  );
};