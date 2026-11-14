"use client";
import { useState, useRef, useEffect} from "react";
import { Star,MoreHorizontal } from "lucide-react";

export type Priority = "High" | "Medium" | "Low";
export type Status =  "Backlog" | "In Progress" | "In Review" | "Done";
export const allStatuses: Status[] = ["Backlog", "In Progress", "In Review", "Done"];

export type BoardTask = {
  id: number;
  title: string;
  tags: string[];
  priority: Priority;
  date: string;
  status: Status;
  assignee: { initials: string; color: string }; 
};
type Props = BoardTask & {
  onMove: (status: Status) => void;
  onDelete: () => void;
};


const priorityStyle: Record<Priority, { badge: string; dot: string }> = {
  High: { badge: "bg-danger/15 text-danger", dot: "bg-danger" },
  Medium: { badge: "bg-warn/15 text-warn", dot: "bg-warn" },
  Low: { badge: "bg-ok/15 text-ok", dot: "bg-ok" },
};

export default function TaskCard({title, tags, priority, status, date, assignee, onMove, onDelete,
}: Props) {
  const p = priorityStyle[priority];
  const [open, setOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!open) return;
    const onClick = (e: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(e.target as Node)) setOpen(false);
    };
    const onKey = (e: KeyboardEvent) => e.key === "Escape" && setOpen(false);
    document.addEventListener("mousedown", onClick);
    document.addEventListener("keydown", onKey);
    return () => {
      document.removeEventListener("mousedown", onClick);
      document.removeEventListener("keydown", onKey);
    };
  }, [open]);

  return (
    <div className={'rounded-xl border border-line bg-card p-5 transition-all duration-200 hover:-translate-y-0.5 hover:border-accent/40 ${ open ? "z-30": ""}'}>
      <div className="flex items-start justify-between gap-3">
      <h3 className="text-base font-medium leading-snug">{title}</h3>

      <div ref={menuRef} className="relative shrink-0">
    <button
      onClick={() => setOpen((o) => !o)}
      aria-label="Task actions"
      className="rounded-md p-1 text-muted transition-colors hover:bg-white/5 hover:text-fg"
    >
      <MoreHorizontal size={18} />
    </button>

    {open && (
      <div className="absolute right-0 top-9 z-40 w-52 rounded-xl border border-line bg-[#171722] p-1.5 shadow-xl shadow-black/40">
        {allStatuses
          .filter((s) => s !== status)
          .map((s) => (
            <button
              key={s}
              onClick={() => {
                onMove(s);
                setOpen(false);
              }}
              className="block w-full rounded-lg px-3 py-2 text-left text-sm text-muted transition-colors hover:bg-white/5 hover:text-fg"
            >
              Move to {s}
            </button>
          ))}

        <div className="my-1.5 border-t border-line" />

        <button
          onClick={() => {
            onDelete();
            setOpen(false);
          }}
          className="block w-full rounded-lg px-3 py-2 text-left text-sm text-danger transition-colors hover:bg-danger/10"
        >
          Delete
        </button>
      </div>
    )}
  </div>
  </div>

     
      <div className="mt-4 flex flex-wrap gap-2">
        {tags.map((tag) => (
          <span
            key={tag}
            className="rounded-md bg-accent/15  px-2.5 py-1 text-[13px] font-medium text-accent"
          >
            {tag}
          </span>
        ))}
      </div>

     
      <div className="mt-5 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <span
            className={`flex h-10 w-10 items-center justify-center rounded-full px-3 py-1.5 text-[13px] font-semibold text-white ${assignee.color}`}
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

        <div className="flex items-center gap-2 text-[13px] text-muted">
          <Star size={16} />
          {date}
        </div>
      </div>
    </div>
  );
}