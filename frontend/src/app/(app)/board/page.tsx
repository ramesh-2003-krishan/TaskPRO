"use client";
import { useState } from "react";
import TaskCard, { type BoardTask, type Status } from "@/components/board/TaskCard";

const columns: { status: Status; dot: string }[] = [
  { status: "Backlog", dot: "bg-[#5b5b78]" },
  { status: "In Progress", dot: "bg-accent" },
  { status: "In Review", dot: "bg-warn" },
  { status: "Done", dot: "bg-ok" },
];

const initialTasks: BoardTask[] = [
  { id: 1, title: "Fix crash on iOS 17 deep links", tags: ["Bug", "iOS"], priority: "High", status: "Backlog", date: "Aug 9", assignee: { initials: "SR", color: "bg-ok" } },
  { id: 2, title: "Write API documentation", tags: ["Docs"], priority: "Low", status: "Backlog", date: "Aug 18", assignee: { initials: "RK", color: "bg-info" } },
  { id: 3, title: "Redesign navigation architecture", tags: ["Design"], priority: "Medium", status: "In Progress", date: "Aug 10", assignee: { initials: "AP", color: "bg-accent" } },
  { id: 4, title: "Create component library tokens", tags: ["Design"], priority: "Medium", status: "In Review", date: "Aug 14", assignee: { initials: "MC", color: "bg-pink" } },
];


export default function BoardPage() {
    const [tasks, setTasks] = useState<BoardTask[]>(initialTasks);

    const moveTask = (id: number, status: Status) =>
    setTasks((prev) => prev.map((t) => (t.id === id ? { ...t, status } : t)));

    const deleteTask = (id: number) =>
    setTasks((prev) => prev.filter((t) => t.id !== id));

 return (
    <section className="flex items-start gap-6 overflow-x-auto pb-4">
      {columns.map((col) => {
        const items = tasks.filter((t) => t.status === col.status);
        return (
          <div key={col.status} className="w-[380px] shrink-0">
            <div className="mb-4 flex items-center gap-3">
              <span className={`h-2 w-2 rounded-full ${col.dot}`} />
              <h1 className="font-semibold">{col.status}</h1>
              <span className="text-sm text-muted">{items.length}</span>
            </div>

            <div className="flex flex-col gap-3">
              {items.map((task) => (
                <TaskCard
                  key={task.id}
                  {...task}
                  onMove={(status) => moveTask(task.id, status)}
                  onDelete={() => deleteTask(task.id)}
                />
              ))}
              {items.length === 0 && (
                <div className="rounded-xl border border-dashed border-line p-6 text-center text-sm text-muted">
                  No tasks
                </div>
              )}
            </div>
          </div>
        );
      })}
    </section>
  );
}