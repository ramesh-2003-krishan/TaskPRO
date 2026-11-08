"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import{
    Calendar,
    CheckSquare,
    Clipboard,
    LayoutDashboard,
    LayoutGrid,
    Plus,
    Settings,
    User,
    Zap,

}from "lucide-react";

import{mockProject, mockUser} from "@/lib/mock";

const navigation = [
    { name: "Dashboard", href: "/dashboard", icon: LayoutDashboard },
    {name: "MyTasks", href: "/my-tasks", icon: CheckSquare},
    {name:"Board", href:"/board", icon:LayoutGrid},
    {name:"Team", href:"/team", icon:User},
    {name:"Calendar", href:"/calendar", icon:Calendar},
];

export default function Sidebar() {
    const pathname = usePathname();
    return (
    <aside className="flex h-full w-55 shrink-0 flex-col border-r border-line bg-sidebar">
      
      <div className="flex h-15.5 items-center justify-between border-b border-line px-5">
        <div className="flex items-center gap-2.5">
          <div className="flex h-8 w-8 items-center justify-center rounded-lg bg-accent">
            <Zap size={16} className="text-white" />
          </div>
          <span className="text-[15px] font-semibold">TaskPRO</span>
        </div>
        <span className="rounded bg-accent/15 px-1.5 py-0.5 text-[10px] font-semibold tracking-wide text-accent">
          PRO
        </span>
      </div>

     
      <nav className="space-y-1 px-3 pt-4">
        {navigation.map(({ href, name, icon: Icon }) => {
          const active = pathname === href || pathname.startsWith(href + "/");
          return (
            <Link
              key={href}
              href={href}
              className={`flex items-center gap-3 rounded-lg px-3 py-2 text-sm transition-colors ${
                active
                  ? "bg-accent/15 font-medium text-accent"
                  : "text-muted hover:bg-white/5 hover:text-fg"
              }`}
            >
              <Icon size={16} />
              {name}
            </Link>
          );
        })}
      </nav>

      
      <div className="mt-6 flex-1 overflow-y-auto px-3">
        <div className="mb-2 flex items-center justify-between px-3">
          <span className="text-[11px] font-semibold tracking-wider text-muted">PROJECTS</span>
          <button aria-label="New project" className="text-muted hover:text-fg">
            <Plus size={14} />
          </button>
        </div>
        <ul className="space-y-0.5">
          {mockProject.map((p) => (
            <li key={p.id}>
              <button className="flex w-full items-center gap-2.5 rounded-lg px-3 py-2 text-left text-sm text-muted hover:bg-white/5 hover:text-fg">
                <span className="h-2 w-2 rounded-full bg-accent" />
                <span className="flex-1 truncate">{p.name}</span>
              </button>
            </li>
          ))}
        </ul>
      </div>

     
      <div className="border-t border-line px-3 py-3">
        <button className="flex w-full items-center gap-3 rounded-lg px-3 py-2 text-sm text-muted hover:bg-white/5 hover:text-fg">
          <Settings size={16} />
          Settings
        </button>
        <div className="mt-2 flex items-center gap-3 px-3 py-2">
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-accent text-xs font-semibold text-white">
            {mockUser.name
              .split(" ")
              .map((part) => part[0])
              .join("")
              .slice(0, 2)
              .toUpperCase()}
          </div>
          <div className="leading-tight">
            <div className="text-sm font-medium">{mockUser.name}</div>
            <div className="text-xs text-muted">{mockUser.plan}</div>
          </div>
        </div>
      </div>
    </aside>
  );
}