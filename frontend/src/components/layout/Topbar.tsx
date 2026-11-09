"use client"

import { useState } from "react";
import {Bell, Plus, Search} from "lucide-react";

const views = [
    "Dashboard",
    "Board",
    "List",
] as const;

export default function Topbar() {
    const[view, setView] = useState<(typeof views)[number]>("Dashboard");

    return (
        <header className="flex h-15.5 shrink-0 items-center justify-between gap-4 border-b border-line bg-page px-6">
    
      <div className="flex h-9 w-full max-w-[320px] items-center gap-2 rounded-lg border border-line bg-card px-3">
        <Search size={15} className="text-muted" />
        <input
          placeholder="Search tasks, projects, tags..."
          className="w-full bg-transparent text-sm text-fg outline-none placeholder:text-muted"
        />
      </div>

      <div className="flex items-center gap-4">
       
        <div className="flex rounded-lg border border-line bg-card p-0.5">
          {views.map((v) => (
            <button
              key={v}
              onClick={() => setView(v)}
              className={`rounded-md px-3.5 py-1.5 text-sm transition-colors ${
                view === v ? "bg-accent font-medium text-white" : "text-muted hover:text-fg"
              }`}
            >
              {v}
            </button>
          ))}
        </div>

    
        <button aria-label="Notifications" className="relative text-muted hover:text-fg">
          <Bell size={18} />
          <span className="absolute -right-0.5 -top-0.5 h-2 w-2 rounded-full bg-accent" />
        </button>

        <button className="flex h-9 items-center gap-2 rounded-lg bg-accent px-4 text-sm font-medium text-white hover:brightness-110">
          <Plus size={15} />
          Add Task
        </button>
      </div>
    </header>
  );
}
