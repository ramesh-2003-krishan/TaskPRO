
"use client";

import { useEffect, useState } from "react";
import {
  ChevronLeft,
  ChevronRight,
  CalendarDays,
  Clock3,
  CheckCircle2,
  Plus,
  ArrowUpRight,
} from "lucide-react";
import { motion, AnimatePresence } from "framer-motion";

type Task = {
  id: number;
  title: string;
  date: string;
  time: string;
  status: "Completed" | "In Progress" | "Pending";
  priority: "High" | "Medium" | "Low";
};

const tasks: Task[] = [
  {
    id: 1,
    title: "Complete Dashboard UI",
    date: "2026-11-26",
    time: "09:00 AM",
    status: "In Progress",
    priority: "High",
  },
  {
    id: 2,
    title: "Team Knowledge Sharing",
    date: "2026-11-26",
    time: "11:30 AM",
    status: "Completed",
    priority: "Medium",
  },
  {
    id: 3,
    title: "Update Calendar Components",
    date: "2026-11-27",
    time: "10:00 AM",
    status: "Pending",
    priority: "High",
  },
  {
    id: 4,
    title: "Review Project Progress",
    date: "2026-11-28",
    time: "02:00 PM",
    status: "In Progress",
    priority: "Medium",
  },
  {
    id: 5,
    title: "Fix Responsive Layout",
    date: "2026-11-30",
    time: "04:00 PM",
    status: "Pending",
    priority: "Low",
  },
];

const weekDays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

export default function CalendarPage() {
  const [currentDate, setCurrentDate] = useState(new Date(2026, 10, 26));
  const [selectedDate, setSelectedDate] = useState(
    "2026-11-26"
  );

  const year = currentDate.getFullYear();
  const month = currentDate.getMonth();

  const firstDay = new Date(year, month, 1).getDay();
  const daysInMonth = new Date(year, month + 1, 0).getDate();

  const formatDate = (day: number) => {
    const m = String(month + 1).padStart(2, "0");
    const d = String(day).padStart(2, "0");
    return `${year}-${m}-${d}`;
  };

  const selectedTasks = tasks.filter(
    (task) => task.date === selectedDate
  );

  const [todayString, setTodayString] = useState("");

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      const today = new Date();
      setTodayString(
        [
          today.getFullYear(),
          String(today.getMonth() + 1).padStart(2, "0"),
          String(today.getDate()).padStart(2, "0"),
        ].join("-")
      );
    }, 0);

    return () => window.clearTimeout(timeoutId);
  }, []);

  const changeMonth = (direction: number) => {
    setCurrentDate(new Date(year, month + direction, 1));
  };

  const statusColor = {
    Completed: "bg-emerald-400",
    "In Progress": "bg-blue-400",
    Pending: "bg-amber-400",
  };

  const priorityColor = {
    High: "text-rose-400 bg-rose-400/10",
    Medium: "text-amber-400 bg-amber-400/10",
    Low: "text-emerald-400 bg-emerald-400/10",
  };

  const completed = tasks.filter(
    (task) => task.status === "Completed"
  ).length;

  return (
    <main className="min-h-screen bg-[#080b14] text-white p-4 md:p-8 relative overflow-hidden">

      
      <div className="absolute top-[-150px] left-[-100px] w-96 h-96 bg-violet-600/20 rounded-full blur-[120px] pointer-events-none" />
      <div className="absolute bottom-[-100px] right-[-100px] w-96 h-96 bg-blue-500/20 rounded-full blur-[120px] pointer-events-none" />

      <div className="relative z-10 max-w-7xl mx-auto space-y-8">

      
        <motion.div
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          className="flex flex-col md:flex-row md:items-center justify-between gap-5"
        >
          <div>
            <div className="flex items-center gap-2 text-violet-400 mb-2">
              <CalendarDays size={19} />
              <span className="text-sm font-medium tracking-widest uppercase">
                Workspace / Calendar
              </span>
            </div>

            <h1 className="text-3xl md:text-4xl font-bold tracking-tight">
              My Calendar
            </h1>

            <p className="text-gray-400 mt-2">
              Organize your schedule and stay on top of your tasks.
            </p>
          </div>

          <button className="flex items-center justify-center gap-2 px-5 py-3 rounded-xl bg-violet-600 hover:bg-violet-500 transition-all duration-300 shadow-lg shadow-violet-600/20 hover:scale-105">
            <Plus size={18} />
            Add New Task
          </button>
        </motion.div>

       
        <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
          {[
            {
              label: "Total Tasks",
              value: tasks.length,
              color: "text-violet-400",
              icon: <CalendarDays />,
            },
            {
              label: "Completed",
              value: completed,
              color: "text-emerald-400",
              icon: <CheckCircle2 />,
            },
            {
              label: "Remaining",
              value: tasks.length - completed,
              color: "text-amber-400",
              icon: <Clock3 />,
            },
          ].map((stat, index) => (
            <motion.div
              key={stat.label}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: index * 0.1 }}
              whileHover={{ scale: 1.06 }}
              whileTap={{ scale: 0.94 }}
              className="bg-white/[0.04] backdrop-blur-2xl border border-white/10 rounded-2xl p-5 hover:bg-white/[0.07] transition-all duration-300 hover:bg-white/[0.07] hover:border-white/10"
            >
              <div className="flex justify-between items-center">
                <div>
                  <p className="text-sm text-gray-400">{stat.label}</p>
                  <h2 className={`text-3xl font-bold mt-2 ${stat.color}`}>
                    {stat.value}
                  </h2>
                </div>
                <div className={`${stat.color} bg-white/[0.05] p-3 rounded-xl`}>
                  {stat.icon}
                </div>
              </div>
            </motion.div>
          ))}
        </div>

        
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">

          
          <motion.section
            initial={{ opacity: 0, scale: 0.97 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5 }}
            className="lg:col-span-2 bg-white/[0.045] backdrop-blur-2xl border border-white/10 rounded-3xl p-5 md:p-7 shadow-2xl"
          >

           
            <div className="flex items-center justify-between mb-8">
              <div>
                <h2 className="text-2xl font-bold">
                  {currentDate.toLocaleString("en-US", {
                    month: "long",
                    year: "numeric",
                  })}
                </h2>
                <p className="text-gray-500 text-sm mt-1">
                  Select a date to view tasks
                </p>
              </div>

              <div className="flex items-center gap-2">
                <button
                  onClick={() => setCurrentDate(new Date())}
                  className="hidden sm:block px-3 py-2 text-sm rounded-lg border border-white/10 hover:bg-white/10 transition"
                >
                  Today
                </button>

                <button
                  onClick={() => changeMonth(-1)}
                  className="p-2 rounded-xl border border-white/10 hover:bg-white/10 transition"
                >
                  <ChevronLeft size={20} />
                </button>

                <button
                  onClick={() => changeMonth(1)}
                  className="p-2 rounded-xl border border-white/10 hover:bg-white/10 transition"
                >
                  <ChevronRight size={20} />
                </button>
              </div>
            </div>

           
            <div className="grid grid-cols-7 mb-3">
              {weekDays.map((day) => (
                <div
                  key={day}
                  className="text-center text-xs sm:text-sm font-semibold text-gray-500 py-3"
                >
                  {day}
                </div>
              ))}
            </div>

            
            <AnimatePresence mode="wait">
              <motion.div
                key={`${year}-${month}`}
                initial={{ opacity: 0, x: 15 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: -15 }}
                transition={{ duration: 0.2 }}
                className="grid grid-cols-7 gap-1 sm:gap-2"
              >
                {Array.from({ length: firstDay }).map((_, index) => (
                  <div key={`empty-${index}`} className="aspect-square" />
                ))}

                {Array.from({ length: daysInMonth }).map((_, index) => {
                  const day = index + 1;
                  const dateString = formatDate(day);
                  const dayTasks = tasks.filter(
                    (task) => task.date === dateString
                  );

                  const isToday = dateString === todayString;
                  const isSelected = dateString === selectedDate;

                  return (
                    <motion.button
                      key={day}
                      whileHover={{ scale: 1.06 }}
                      whileTap={{ scale: 0.94 }}
                      onClick={() => setSelectedDate(dateString)}
                      className={`aspect-square rounded-xl sm:rounded-2xl flex flex-col items-center justify-center gap-1 border transition-all duration-200
                        ${
                          isSelected
                            ? "bg-violet-600/30 border-violet-400 shadow-lg shadow-violet-500/10"
                            : isToday
                            ? "border-violet-400/60 bg-violet-500/10"
                            : "border-transparent hover:bg-white/[0.07] hover:border-white/10"
                        }`}
                    >
                      <span
                        className={`text-sm sm:text-base font-semibold ${
                          isSelected
                            ? "text-violet-300"
                            : isToday
                            ? "text-violet-300"
                            : "text-gray-300"
                        }`}
                      >
                        {day}
                      </span>

                      <div className="flex gap-1 h-2 items-center">
                        {dayTasks.slice(0, 3).map((task) => (
                          <span
                            key={task.id}
                            className={`w-1.5 h-1.5 rounded-full ${statusColor[task.status]}`}
                          />
                        ))}
                      </div>
                    </motion.button>
                  );
                })}
              </motion.div>
            </AnimatePresence>

          
            <div className="flex flex-wrap gap-5 mt-7 pt-5 border-t border-white/10">
              {Object.entries(statusColor).map(([status, color]) => (
                <div key={status} className="flex items-center gap-2">
                  <span className={`w-2 h-2 rounded-full ${color}`} />
                  <span className="text-xs text-gray-400">{status}</span>
                </div>
              ))}
            </div>
          </motion.section>

          
          <motion.aside
            initial={{ opacity: 0, x: 20 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.2 }}
            className="bg-white/[0.045] backdrop-blur-2xl border border-white/10 rounded-3xl p-5 md:p-6"
          >
            <div className="flex items-center justify-between mb-6">
              <div>
                <h2 className="text-xl font-bold">Schedule</h2>
                <p className="text-sm text-gray-500 mt-1">
                  {new Date(`${selectedDate}T00:00:00`).toLocaleDateString(
                    "en-US",
                    {
                      weekday: "long",
                      month: "short",
                      day: "numeric",
                    }
                  )}
                </p>
              </div>
              <span className="text-xs px-3 py-1 rounded-full bg-violet-500/10 text-violet-300">
                {selectedTasks.length} Tasks
              </span>
            </div>

            <div className="space-y-4">
              <AnimatePresence mode="popLayout">
                {selectedTasks.length > 0 ? (
                  selectedTasks.map((task, index) => (
                    <motion.div
                      key={task.id}
                      layout
                      initial={{ opacity: 0, y: 10 }}
                      animate={{ opacity: 1, y: 0 }}
                      exit={{ opacity: 0, scale: 0.9 }}
                      transition={{ delay: index * 0.07 }}
                      whileHover={{ scale: 1.06 }}
                      whileTap={{ scale: 0.94 }}
                      className="p-4 rounded-2xl bg-white/[0.04] border border-white/[0.08] hover:border-violet-400/30 hover:bg-white/[0.07] transition-all group hover:bg-white/[0.07] hover:border-white/10"
                    >
                      <div className="flex justify-between gap-2">
                        <h3 className="text-sm font-semibold group-hover:text-violet-300 transition">
                          {task.title}
                        </h3>
                        <ArrowUpRight
                          size={16}
                          className="text-gray-500 shrink-0"
                        />
                      </div>

                      <div className="flex items-center gap-2 text-xs text-gray-500 mt-3">
                        <Clock3 size={13} />
                        {task.time}
                      </div>

                      <div className="flex items-center justify-between mt-4 gap-2">
                        <span className="flex items-center gap-2 text-xs text-gray-400">
                          <span className={`w-2 h-2 rounded-full ${statusColor[task.status]}`} />
                          {task.status}
                        </span>

                        <span className={`text-[10px] px-2 py-1 rounded-full ${priorityColor[task.priority]}`}>
                          {task.priority}
                        </span>
                      </div>
                    </motion.div>
                  ))
                ) : (
                  <motion.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    className="py-12 text-center"
                  >
                    <div className="w-14 h-14 mx-auto rounded-2xl bg-white/[0.05] flex items-center justify-center mb-4">
                      <CalendarDays className="text-gray-500" size={25} />
                    </div>
                    <h3 className="text-sm font-semibold text-gray-300">
                      No tasks scheduled
                    </h3>
                    <p className="text-xs text-gray-500 mt-2">
                      Enjoy your free time!
                    </p>
                  </motion.div>
                )}
              </AnimatePresence>
            </div>
          </motion.aside>

        </div>
      </div>
    </main>
  );
}
