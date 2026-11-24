"use client";

import ProgressCards from "@/components/myTask/StatCards";
import { mockProgress, mockProjectCards } from "@/lib/mock";
import MyTaskCard, {type ListTask} from "@/components/myTask/MyTaskCard"
import { Activity, useState,useRef } from "react";
import { FolderKanban } from "lucide-react";
import ProjectPieCard from "@/components/myTask/ProjectCard";
import OtherActivity,{type props} from "@/components/myTask/OtherActivity";


const platform = { name: "Platform Redesign", color: "bg-accent" };
const mobile = { name: "Mobile App", color: "bg-ok" };
const api = { name: "API Integration", color: "bg-warn" };
const design = { name: "Design System", color: "bg-pink" };

const colorOf: Record<string, string> = {
  "bg-accent": "var(--accent)",
  "bg-ok": "var(--ok)",
  "bg-warn": "var(--warn)",
  "bg-pink": "var(--pink)",
};


const initialTasks: ListTask[] = [
  { id: 1, title: "Redesign navigation architecture", starred: true, project: platform, priority: "High", status: "In Progress", due: "Aug 10" },
  { id: 2, title: "Implement OAuth 2.0 authentication", starred: false, project: api, priority: "High", status: "In Progress", due: "Aug 12" },
  { id: 3, title: "Create component library tokens", starred: true, project: design, priority: "Medium", status: "In Review", due: "Aug 14"},
  { id: 4, title: "Fix crash on iOS 17 deep links", starred: false, project: mobile, priority: "High", status: "Backlog", due: "Aug 9"},
  { id: 5, title: "Write API documentation", starred: false, project: api, priority: "Medium", status: "Backlog", due: "Aug 18"},
  { id: 6, title: "Migrate database schema v3", starred: false, project: platform, priority: "High", status: "Backlog", due: "Aug 20" },
  { id: 7, title: "A/B test onboarding flow", starred: false, project: mobile, priority: "Medium", status: "In Progress", due: "Aug 15"},
  { id: 8, title: "Accessibility audit — WCAG AA", starred: true, project: design, priority: "Medium", status: "In Review", due: "Aug 11" },
  { id: 9, title: "Set up CI/CD pipeline", starred: false, project: platform, priority: "Low", status: "Done", due: "Aug 5"},
  { id: 10, title: "Performance profiling — dashboard", starred: false, project: platform, priority: "Low", status: "Done", due: "Aug 4"},
  { id: 11, title: "Push notification service", starred: false, project: mobile, priority: "Medium", status: "Backlog", due: "Aug 22"},
];

const initialOtherActivity: props[] = [
  { Activity: "World cup match", Date: "2026/12/21", Time: "9:30 AM" },
];

export default function myTaskPage (){
    const [tasks, setTasks] = useState<ListTask[]>(initialTasks);
     const[formOpen, setFormOpen] = useState(false);
  
    const formRef = useRef<HTMLDivElement>(null);
     
    
      const toggleStar = (id: number) =>
        setTasks((prev) => prev.map((t) => (t.id === id ? { ...t, starred: !t.starred } : t)));
    
      const deleteTask = (id: number) => setTasks((prev) => prev.filter((t) => t.id !== id));

      const editTask = (id: number, updatedFields: Partial<ListTask> = {}) => {
      setTasks((prev) =>
    prev.map((t) => (t.id === id ? { ...t, ...updatedFields } : t))
  );
};
    return(
        <div className="space-y-6">
              <section className="grid grid-cols-[repeat(auto-fit,minmax(220px,1fr))] gap-4">
                <ProgressCards {...mockProgress}/>
              </section>

              <section className="rounded-xl border border-line bg-card p-6 transition-transform duration-100 hover:scale+[0.5] hover:shadow-lg hover:shadow-accent">
                <h1 className="font-semibold mb-6">My Tasks</h1>
                <MyTaskCard tasks={tasks} onToggleStar={toggleStar} onDelete={deleteTask} onEdit={editTask} />
              </section>

              <section className="grid grid-cols-[repeat(auto-fit,minmax(220px,1fr))] gap-4">
                <div className="grid grid-cols-[repeat(auto-fit,minmax(220px,1fr))] gap-4">
                     <ProjectPieCard
                    data={mockProjectCards.map((p) => ({
                    name: p.name,
                    value: p.openTasks,
                    color: colorOf[p.color] ?? "var(--muted)",
                     }))}
                   />
                </div>
                <div className="grid grid-cols-[repeat(auto-fit,minmax(220px,1fr))] gap-4 ">
                  
                   <OtherActivity activities={initialOtherActivity} />
                </div>
             
              </section>

        </div>
      
    );
}