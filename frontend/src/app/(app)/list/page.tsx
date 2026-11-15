"use client";

import { useState } from "react";
import TaskTable, { type ListTask } from "@/components/list/TaskTable";

const platform = { name: "Platform Redesign", color: "bg-accent" };
const mobile = { name: "Mobile App", color: "bg-ok" };
const api = { name: "API Integration", color: "bg-warn" };
const design = { name: "Design System", color: "bg-pink" };

const AP = { initials: "AP", color: "bg-accent" };
const JL = { initials: "JL", color: "bg-warn" };
const MC = { initials: "MC", color: "bg-pink" };
const SR = { initials: "SR", color: "bg-ok" };
const RK = { initials: "RK", color: "bg-info" };


const initialTasks: ListTask[] = [
  { id: 1, title: "Redesign navigation architecture", starred: true, project: platform, priority: "High", status: "In Progress", due: "Aug 10", assignee: AP },
  { id: 2, title: "Implement OAuth 2.0 authentication", starred: false, project: api, priority: "High", status: "In Progress", due: "Aug 12", assignee: JL },
  { id: 3, title: "Create component library tokens", starred: true, project: design, priority: "Medium", status: "In Review", due: "Aug 14", assignee: MC },
  { id: 4, title: "Fix crash on iOS 17 deep links", starred: false, project: mobile, priority: "High", status: "Backlog", due: "Aug 9", assignee: SR },
  { id: 5, title: "Write API documentation", starred: false, project: api, priority: "Medium", status: "Backlog", due: "Aug 18", assignee: RK },
  { id: 6, title: "Migrate database schema v3", starred: false, project: platform, priority: "High", status: "Backlog", due: "Aug 20", assignee: JL },
  { id: 7, title: "A/B test onboarding flow", starred: false, project: mobile, priority: "Medium", status: "In Progress", due: "Aug 15", assignee: AP },
  { id: 8, title: "Accessibility audit — WCAG AA", starred: true, project: design, priority: "Medium", status: "In Review", due: "Aug 11", assignee: MC },
  { id: 9, title: "Set up CI/CD pipeline", starred: false, project: platform, priority: "Low", status: "Done", due: "Aug 5", assignee: SR },
  { id: 10, title: "Performance profiling — dashboard", starred: false, project: platform, priority: "Low", status: "Done", due: "Aug 4", assignee: RK },
  { id: 11, title: "Push notification service", starred: false, project: mobile, priority: "Medium", status: "Backlog", due: "Aug 22", assignee: JL },
];

export default function ListPage() {
  const [tasks, setTasks] = useState<ListTask[]>(initialTasks);

  const toggleStar = (id: number) =>
    setTasks((prev) => prev.map((t) => (t.id === id ? { ...t, starred: !t.starred } : t)));

  const deleteTask = (id: number) => setTasks((prev) => prev.filter((t) => t.id !== id));

  return <TaskTable tasks={tasks} onToggleStar={toggleStar} onDelete={deleteTask} />;
}