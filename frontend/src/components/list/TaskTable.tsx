import { Star, Trash2 } from "lucide-react";
import type { Priority, Status } from "@/components/board/TaskCard";

export type ListTask = {
  id: number;
  title: string;
  starred: boolean;
  project: { name: string; color: string }; 
  priority: Priority;
  status: Status;
  due: string;
  assignee: { initials: string; color: string };
};

type Props = {
  tasks: ListTask[];
  onToggleStar: (id: number) => void;
  onDelete: (id: number) => void;
};

const priorityStyle: Record<Priority, { badge: string; dot: string }> = {
  High: { badge: "bg-danger/15 text-danger", dot: "bg-danger" },
  Medium: { badge: "bg-warn/15 text-warn", dot: "bg-warn" },
  Low: { badge: "bg-ok/15 text-ok", dot: "bg-ok" },
};

const statusStyle: Record<Status, string> = {
  "In Progress": "bg-accent/15 text-accent",
  "In Review": "bg-warn/15 text-warn",
  Backlog: "bg-white/5 text-[#6b6b85]",
  Done: "bg-ok/15 text-ok",
};

const headers = ["TASK", "PROJECT", "PRIORITY", "STATUS", "DUE", "ASSIGNEE"];

export default function TaskTable({ tasks, onToggleStar, onDelete }: Props) {
  return (
    <div className="overflow-hidden rounded-xl border border-line bg-card">
      <div className="overflow-x-auto">
        <table className="w-full min-w-[860px] text-left">
          <thead>
            <tr className="border-b border-line">
              {headers.map((h) => (
                <th key={h} className="px-4 py-4 text-[11px] font-semibold tracking-wider text-muted first:pl-5">
                  {h}
                </th>
              ))}
            </tr>
          </thead>

          <tbody>
            {tasks.map((t) => {
              const p = priorityStyle[t.priority];
              return (
                <tr
                  key={t.id}
                  className="group border-b border-line last:border-b-0 transition-colors hover:bg-white/[0.03]"
                >
                 
                  <td className="py-3.5 pl-5 pr-4">
                    <div className="flex items-center gap-3">
                      <button
                        onClick={() => onToggleStar(t.id)}
                        aria-label={t.starred ? "Remove star" : "Add star"}
                        className={`transition-opacity ${
                          t.starred ? "opacity-100" : "opacity-0 group-hover:opacity-100"
                        }`}
                      >
                        <Star
                          size={15}
                          className={t.starred ? "fill-warn text-warn" : "text-muted hover:text-warn"}
                        />
                      </button>
                      <span className="text-[15px]">{t.title}</span>
                    </div>
                  </td>

                 
                  <td className="px-4 py-3.5">
                    <span className="flex items-center gap-2 text-sm text-muted">
                      <span className={`h-1.5 w-1.5 rounded-full ${t.project.color}`} />
                      {t.project.name}
                    </span>
                  </td>

                  
                  <td className="px-4 py-3.5">
                    <span className={`inline-flex items-center gap-1.5 rounded-md px-2 py-1 text-xs font-medium ${p.badge}`}>
                      <span className={`h-1.5 w-1.5 rounded-full ${p.dot}`} />
                      {t.priority}
                    </span>
                  </td>

                 
                  <td className="px-4 py-3.5">
                    <span className={`inline-flex rounded-md px-2 py-1 text-xs font-medium ${statusStyle[t.status]}`}>
                      {t.status}
                    </span>
                  </td>

                 
                  <td className="px-4 py-3.5 text-sm text-muted">{t.due}</td>

                  
                  <td className="px-4 py-3.5">
                    <div className="flex items-center gap-3">
                      <span
                        className={`flex h-7 w-7 items-center justify-center rounded-full text-[10px] font-semibold text-white ${t.assignee.color}`}
                      >
                        {t.assignee.initials}
                      </span>
                      <button
                        onClick={() => onDelete(t.id)}
                        aria-label="Delete task"
                        className="text-muted opacity-0 transition-opacity hover:text-danger group-hover:opacity-100"
                      >
                        <Trash2 size={14} />
                      </button>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>

      {tasks.length === 0 && <p className="p-8 text-center text-sm text-muted">No tasks yet.</p>}
    </div>
  );
}