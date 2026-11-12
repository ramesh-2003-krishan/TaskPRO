import type { LucideIcon } from "lucide-react";

type props = {
    id : string;
    name : string;
    description : string;
    icon : LucideIcon;
    tone : "accent" | "okay" | "danger"; 
}

const tones = {
    accent: "bg-accent/15 text-accent",
    okay: "bg-ok/15 text-ok",
    danger: "bg-danger/15 text-danger",
}

export default function ProjectCard({id, name, description, icon:Icon, tone}:props){
    return(
       <div className="rounded-xl border border-line bg-card p-5 transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
      <div className="flex items-start justify-between">
        <span className="text-sm text-muted">{name}</span>
        <div className={`flex h-9 w-9 items-center justify-center rounded-lg ${tones[tone]}`}>
          <Icon size={17} />
        </div>
      </div>
      <div className="mt-3 font-display text-[32px] font-bold leading-none">{id}</div>
      <div className="mt-2 text-xs text-muted">{description}</div>
    </div>
    );
}
