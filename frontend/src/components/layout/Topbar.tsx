"use client"

import { useEffect, useRef, useState } from "react";
import {Bell, Plus, Search} from "lucide-react";
import Link from "next/link";
import { usePathname } from "next/navigation";
import NotificationsPanel from "../notification/NotificationBar";


const views = [
    "Dashboard",
    "Board",
    "List",
] as const;

export default function Topbar() {
    const[view, setView] = useState<(typeof views)[number]>();
    const [notificationsOpen, setNotificationsOpen] = useState(false);
    const pathname = usePathname();
    const notificationsRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
      if (!notificationsOpen) return;

      const handlePointerDown = (event: PointerEvent) => {
        if (event.target instanceof Node && !notificationsRef.current?.contains(event.target)) {
          setNotificationsOpen(false);
        }
      };

      document.addEventListener("pointerdown", handlePointerDown);
      return () => document.removeEventListener("pointerdown", handlePointerDown);
    }, [notificationsOpen]);

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
          {views.map((v) => {
            const href =
              v === "Board" ? "/board" : v === "Dashboard" ? "/dashboard" : v === "List" ? "/list":null;
            const active = href
              ? pathname === href || pathname.startsWith(`${href}/`)
              : view === v;
            const className = `rounded-md px-3.5 py-1.5 text-sm transition-colors ${
              active ? "bg-accent font-medium text-white" : "text-muted hover:text-fg"
            }`;

            return href ? (
              <Link key={v} href={href} className={className}>
                {v}
              </Link>
            ) : (
              <button
                key={v}
                onClick={() => setView(v)}
                className={className}
                
              >
                {v}
              </button>
            );
          })}
        </div>

    
        <div ref={notificationsRef} className="relative">
          <button
            aria-label="Notifications"
            aria-expanded={notificationsOpen}
            className="relative text-muted hover:text-fg"
            onClick={ 
              () => setNotificationsOpen((open) => !open)
            }
            
          >
            <Bell size={18} />
            <span className="absolute -right-0.5 -top-0.5 h-2 w-2 rounded-full bg-accent" />
          </button>
          {notificationsOpen && (
            <div className="absolute right-0 top-full z-50 mt-2 w-[min(24rem,calc(100vw-2rem))]">
              <NotificationsPanel />
            </div>
          )}
        </div>

        <button className="flex h-9 items-center gap-2 rounded-lg bg-accent px-4 text-sm font-medium text-white hover:brightness-110">
          <Plus size={15} />
          Add Task
        </button>
      </div>
    </header>
  );
}
