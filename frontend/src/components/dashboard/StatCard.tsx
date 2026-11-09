import type {LucideIcon} from "lucide-react";

type props = {
    label: string;
    value: string;
    caption: string;
    icon: LucideIcon;
    tone: "accent" | "ok" | "danger";
};

const tones = {
  accent: "bg-accent/15 text-accent",
  ok: "bg-ok/15 text-ok",
  danger: "bg-danger/15 text-danger",
};

export default function StatCard({label, value, caption, icon: Icon, tone}: props) {
    return (
        <div className="rounded-xl border border-line bg-card p-5">
      <div className="flex items-start justify-between">
        <span className="text-sm text-muted">{label}</span>
        <div className={`flex h-9 w-9 items-center justify-center rounded-lg ${tones[tone]}`}>
          <Icon size={17} />
        </div>
      </div>
      <div className="mt-3 font-display text-[32px] font-bold leading-none">{value}</div>
      <div className="mt-2 text-xs text-muted">{caption}</div>
    </div>
  );
}
