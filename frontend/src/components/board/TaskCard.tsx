import { Star } from "lucide-react";

export type Priority = "High" | "Medium" | "Low";

export type BoardTask = {
  id: number;
  title: string;
  tags: string[];
  priority: Priority;
  date: string;
  assignee: { initials: string; color: string }; 
};


const priorityStyle: Record<Priority, { badge: string; dot: string }> = {
  High: { badge: "bg-danger/15 text-danger", dot: "bg-danger" },
  Medium: { badge: "bg-warn/15 text-warn", dot: "bg-warn" },
  Low: { badge: "bg-ok/15 text-ok", dot: "bg-ok" },
};

export default function TaskCard({ title, tags, priority, date, assignee }: BoardTask) {
  const p = priorityStyle[priority];

  return (
    <div className="rounded-xl border border-line bg-card p-4 transition-all duration-200 hover:-translate-y-0.5 hover:border-accent/40">
      
      <h3 className="text-[15px] font-medium leading-snug">{title}</h3>

     
      <div className="mt-3 flex flex-wrap gap-2">
        {tags.map((tag) => (
          <span
            key={tag}
            className="rounded-md bg-accent/15 px-2 py-0.5 text-xs font-medium text-accent"
          >
            {tag}
          </span>
        ))}
      </div>

     
      <div className="mt-4 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <span
            className={`flex h-8 w-8 items-center justify-center rounded-full text-[11px] font-semibold text-white ${assignee.color}`}
          >
            {assignee.initials}
          </span>

          <span
            className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-semibold ${p.badge}`}
          >
            <span className={`h-1.5 w-1.5 rounded-full ${p.dot}`} />
            {priority}
          </span>
        </div>

        <div className="flex items-center gap-2 text-xs text-muted">
          <Star size={14} />
          {date}
        </div>
      </div>
    </div>
  );
}