
import { ProfileDetail } from "./ProfileCard";
import { TrendingUp, CheckCircle2, ClipboardList } from "lucide-react";

type Props = {
    members: ProfileDetail[];
};

export default function PerformanceDetail({ members }: Props) {
    return (
        <section className="relative overflow-hidden rounded-3xl border border-white/15 bg-white/[0.04] p-6 shadow-[0_10px_40px_-10px_rgba(0,0,0,0.6),inset_0_1px_0_rgba(255,255,255,0.15)] backdrop-blur-xl ">

            
            <div className="pointer-events-none absolute -right-20 -top-20 h-48 w-48 rounded-full bg-accent/20 blur-3xl" />
            <div className="pointer-events-none absolute -bottom-20 -left-20 h-48 w-48 rounded-full bg-info/15 blur-3xl" />

          
            <div className="relative z-10 mb-6 flex items-center justify-between">
                <div>
                    <h2 className="font-display text-xl font-bold text-white">
                        Performance Rate
                    </h2>
                    <p className="mt-1 text-sm text-white/50">
                        Individual task completion overview
                    </p>
                </div>

                <div className="flex h-11 w-11 items-center justify-center rounded-xl border border-white/15 bg-white/10 text-info backdrop-blur-md">
                    <TrendingUp size={22} />
                </div>
            </div>

            
            <div className="relative z-10 space-y-5">

                {members.length === 0 && (
                    <p className="py-8 text-center text-sm text-white/50">
                        No performance data available.
                    </p>
                )}

                {members.map((m, i) => {
                    const percent =
                        m.AssignedTasks === 0
                            ? 0
                            : Math.round(
                                (m.CompletedTasks / m.AssignedTasks) * 100
                            );

                    return (
                        <div
                            key={m.id}
                            className="group animate-card-in rounded-2xl border border-white/10 bg-white/[0.05] p-4 transition-all duration-300 hover:-translate-y-1 hover:border-white/25 hover:bg-white/[0.09] hover:shadow-[0_12px_30px_-12px_rgba(99,102,241,0.35)] motion-reduce:animate-none"
                            style={{ animationDelay: `${i * 90}ms` }}
                        >
                            
                            <div className="mb-3 flex items-center justify-between gap-3">
                                <div className="flex min-w-0 items-center gap-3">
                                    <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-xl border border-white/15 bg-white/10 font-semibold text-white backdrop-blur-md">
                                        {m.Name.charAt(0).toUpperCase()}
                                    </div>

                                    <div className="min-w-0">
                                        <h3 className="truncate text-sm font-semibold text-white">
                                            {m.Name}
                                        </h3>
                                        <p className="text-xs text-white/45">
                                            Team Member
                                        </p>
                                    </div>
                                </div>

                                <span className="shrink-0 rounded-full border border-white/15 bg-white/10 px-3 py-1 text-sm font-semibold text-white backdrop-blur-md">
                                    {percent}%
                                </span>
                            </div>

                            
                            <div className="h-2 overflow-hidden rounded-full bg-white/10">
                                <div
                                    className="h-full origin-left animate-bar-grow rounded-full bg-gradient-to-r from-accent via-info to-cyan-400 transition-all duration-700 motion-reduce:animate-none"
                                    style={{
                                        width: `${Math.min(100, Math.max(0, percent))}%`,
                                    }}
                                />
                            </div>

                           
                            <div className="mt-3 flex items-center justify-between text-xs">
                                <span className="flex items-center gap-1.5 text-white/50">
                                    <ClipboardList size={14} />
                                    Assigned: {m.AssignedTasks}
                                </span>

                                <span className="flex items-center gap-1.5 text-white/60">
                                    <CheckCircle2 size={14} className="text-emerald-400" />
                                    Completed: {m.CompletedTasks}
                                </span>
                            </div>
                        </div>
                    );
                })}
            </div>
        </section>
    );
}
