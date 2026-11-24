import { Binary } from "lucide-react";

export type ProfileDetail = {
    id : number;
    Name : string;
    Age : string;
    Email : string;
    AssignedTasks : number;
    CompletedTasks : number;
    University : string;
    Profile : string;
};

type props = {
    details : ProfileDetail[];
    onDelete : (id : number) => void;
    onEdit : (id : number) => void;
};

const RowHeaders = ["ID", "Name", "Age", "Email","A-Tasks","C-Task", "University", "Profile"];

export default function ProfileCard({details, onDelete, onEdit}:props){
    return(
        <div className="grid grid-cols-[repeat(auto-fit,minmax(280px,1fr))] gap-6">
  {details.length === 0 && (
    <p className="col-span-full py-10 text-center text-sm text-muted">No profiles yet.</p>
  )}

  {details.map((d, i) => {
    const values: Record<string, string | number> = {
      ID: d.id,
      Name: d.Name,
      Age: d.Age,
      Email: d.Email,
      "A-Tasks": d.AssignedTasks,
      "C-Task": d.CompletedTasks,
      University: d.University,
    };
    const percent = d.AssignedTasks === 0 ? 0 : Math.round((d.CompletedTasks / d.AssignedTasks) * 100);

    return (
     
      <div
        key={d.id}
        className="animate-card-in motion-reduce:animate-none [perspective:1000px]"
        style={{ animationDelay: `${i * 90}ms` }}
      >
        
        <div
          onMouseMove={(e) => {
            const el = e.currentTarget;
            const r = el.getBoundingClientRect();
            const x = e.clientX - r.left;
            const y = e.clientY - r.top;
            el.style.setProperty("--mx", `${x}px`);
            el.style.setProperty("--my", `${y}px`);
            const rx = (0.5 - y / r.height) * 10;
            const ry = (x / r.width - 0.5) * 10;
            el.style.transform = `rotateX(${rx}deg) rotateY(${ry}deg) translateY(-6px) scale(1.02)`;
          }}
          onMouseLeave={(e) => {
            e.currentTarget.style.transform = "";
          }}
          className="group relative isolate overflow-hidden rounded-3xl border border-white/15 bg-white/[0.04] shadow-[0_10px_40px_-10px_rgba(0,0,0,0.6),inset_0_1px_0_rgba(255,255,255,0.18)] transition-[transform,box-shadow] duration-300 ease-out will-change-transform hover:shadow-[0_24px_60px_-12px_rgba(99,102,241,0.45),inset_0_1px_0_rgba(255,255,255,0.25)] motion-reduce:transition-none"
        >
          
          {d.Profile ? (
            <img
              src={d.Profile}
              alt=""
              aria-hidden
              className="absolute inset-0 -z-10 h-full w-full scale-150 object-cover opacity-60 blur-2xl saturate-150"
            />
          ) : (
            <div className="absolute inset-0 -z-10 bg-gradient-to-br from-accent/50 via-pink/30 to-info/40" />
          )}
          <div className="absolute inset-0 -z-10 bg-black/35" />

         
          <div
            className="pointer-events-none absolute inset-0 z-20 opacity-0 transition-opacity duration-300 group-hover:opacity-100"
            style={{
              background:
                "radial-gradient(260px circle at var(--mx, 50%) var(--my, 0%), rgba(255,255,255,0.22), transparent 65%)",
            }}
          />

         
          <div className="relative h-48 overflow-hidden">
            {d.Profile ? (
              <img
                src={d.Profile}
                alt={d.Name}
                className="h-full w-full object-cover transition-transform duration-700 ease-out group-hover:scale-110"
              />
            ) : (
              <div className="flex h-full w-full items-center justify-center bg-white/10 font-display text-5xl font-bold text-white/90">
                {d.Name.charAt(0).toUpperCase()}
              </div>
            )}
            
            <div className="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-white/10" />

          
            <span className="absolute right-3 top-3 rounded-full border border-white/25 bg-white/15 px-3 py-1 text-xs font-medium text-white backdrop-blur-md">
              {percent}% done
            </span>
          </div>

         
          <div className="relative z-10 -mt-6 mx-3 rounded-2xl border border-white/15 bg-white/[0.08] p-4 backdrop-blur-xl shadow-[inset_0_1px_0_rgba(255,255,255,0.2)]">
            <dl className="divide-y divide-white/10">
              {RowHeaders.filter((h) => h !== "Profile").map((h) => (
                <div key={h} className="flex items-center justify-between gap-4 py-2 text-sm">
                  <dt className="shrink-0 text-white/55">{h}</dt>
                  <dd className="min-w-0 truncate text-right font-medium text-white" title={String(values[h])}>
                    {values[h]}
                  </dd>
                </div>
              ))}
            </dl>

          
            <div className="mt-3 h-1.5 overflow-hidden rounded-full bg-white/15">
              <div
                className="h-full origin-left animate-bar-grow rounded-full bg-gradient-to-r from-accent to-info motion-reduce:animate-none"
                style={{ width: `${percent}%` }}
              />
            </div>
          </div>

         
          <div className="relative z-10 flex gap-2 p-3 pt-3">
            <button
              onClick={() => onEdit(d.id)}
              className="flex-1 rounded-xl border border-white/20 bg-white/10 py-2.5 text-sm font-medium text-white backdrop-blur-md transition-all duration-200 hover:bg-white/20 active:scale-95"
            >
              Edit
            </button>
            <button
              onClick={() => onDelete(d.id)}
              className="flex-1 rounded-xl border border-danger/30 bg-danger/15 py-2.5 text-sm font-medium text-red-300 backdrop-blur-md transition-all duration-200 hover:bg-danger/30 active:scale-95"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    );
  })}
</div>
    );
}

