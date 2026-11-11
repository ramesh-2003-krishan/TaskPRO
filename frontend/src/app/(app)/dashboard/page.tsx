import { AlertTriangle, CheckCircle2, CheckSquare, Zap } from "lucide-react";
import StatCard from "@/components/dashboard/StatCard";
import WeeklyProgressCard from "@/components/dashboard/WeeklyProgressCard";
import WeeklyActivityCard from "@/components/dashboard/WeeklyActivityCard";
import { mockProgress, mockWeekly, ActiveTasks } from "@/lib/mock";
import ActiveTaskCard from "@/components/dashboard/ActiveTaskCard";
import ProjectCard from "@/components/dashboard/ProjectCard";

export default function DashboardPage() {
 
  return (
    <div className="space-y-6">
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4">
        <StatCard label="Total Tasks" value="17" caption="Across all projects" icon={CheckSquare} tone="accent" />
        <StatCard label="Completed" value="4" caption="24% completion rate" icon={CheckCircle2} tone="ok" />
        <StatCard label="In Progress" value="4" caption="Active right now" icon={Zap} tone="accent" />
        <StatCard label="High Priority" value="5" caption="Needs attention" icon={AlertTriangle} tone="danger" />
      </div>

    
     <section className="grid grid-cols-1 gap-4 lg:grid-cols-[minmax(0,1fr)_300px]">
        <WeeklyActivityCard
          range="This week"
          days={mockWeekly.map(({ day, completed, Added }) => ({
            day,
            completed,
            added: Added,
          }))}
        />
        <WeeklyProgressCard {...mockProgress} />
      </section>

 <section className="rounded-xl border border-line bg-card p-6">
  <div className="flex items-center justify-between">
    <h2 className="font-semibold">Active Tasks</h2>
    <a href="/my-tasks" className="text-sm text-accent hover:underline">
      View all →
    </a>
  </div>

  <div className="mt-3 divide-y divide-line">
    {ActiveTasks.map((task) => (
      <ActiveTaskCard key={task.task} {...task} />
    ))}
  </div>
  </section>

  <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4">
    <ProjectCard id ="1" name="project 1" description="Task management app" icon={CheckSquare} tone="accent"/>  
    <ProjectCard id="2" name="Project 2" description="Game development" icon={CheckSquare} tone="accent"/>
  </div>
    </div>
    
  );
}
