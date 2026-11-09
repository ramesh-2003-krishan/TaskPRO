import { AlertTriangle, CheckCircle2, CheckSquare, Zap } from "lucide-react";
import StatCard from "@/components/dashboard/StatCard";

export default function DashboardPage() {
 
  return (
    <div className="space-y-6">
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4">
        <StatCard label="Total Tasks" value="17" caption="Across all projects" icon={CheckSquare} tone="accent" />
        <StatCard label="Completed" value="4" caption="24% completion rate" icon={CheckCircle2} tone="ok" />
        <StatCard label="In Progress" value="4" caption="Active right now" icon={Zap} tone="accent" />
        <StatCard label="High Priority" value="5" caption="Needs attention" icon={AlertTriangle} tone="danger" />
      </div>

    
      <section className="rounded-xl border border-line bg-card p-6">
        <h2 className="font-semibold">Weekly Activity</h2>
        <p className="mt-1 text-sm text-muted">Chart comes in a later step.</p>
        <div className="h-48" />
      </section>

      <section className="rounded-xl border border-line bg-card p-6">
        <h2 className="font-semibold">Active Tasks</h2>
        <p className="mt-1 text-sm text-muted">Task list comes in a later step.</p>
      </section>
    </div>
  );
}
