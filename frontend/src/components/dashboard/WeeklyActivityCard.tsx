type Day = { day: string; completed: number; added: number };

type Props = {
  range: string;
  days: Day[]};


export default function WeeklyActivityCard({ range, days }: Props) {
  
  const max = Math.max(...days.map((d) => Math.max(d.completed, d.added)), 1);

  return (
    <section className="flex flex-col rounded-xl border border-line bg-card p-6 transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
      <div className="flex items-start justify-between gap-4">
        <div>
          <h2 className="font-semibold">Weekly Activity</h2>
          <p className="mt-1 text-sm text-muted">{range}</p>
        </div>

        <div className="flex items-center gap-4 text-xs text-muted">
          <span className="flex items-center gap-1.5">
            <span className="h-2 w-2 rounded-full bg-accent" />
            Completed
          </span>
          <span className="flex items-center gap-1.5">
            <span className="h-2 w-2 rounded-full bg-accent/30" />
            Added
          </span>
        </div>
      </div>

     
      <div className="mt-6 flex min-h-42.5 flex-1 items-stretch justify-between gap-2 px-4">
        {days.map((d) => (
          <div key={d.day} className="group flex flex-1 flex-col items-center">
            <div className="relative flex w-full flex-1 items-end justify-center">
              
              <div
                className="absolute bottom-0 w-2.5 rounded-full bg-accent/30"
                style={{ height: `${(d.added / max) * 100}%` }} 
              />
              <div
  className="pointer-events-none absolute left-1/2 z-10 -translate-x-1/2 translate-y-2 scale-75 whitespace-nowrap rounded-lg border border-line bg-page px-3 py-2 text-xs opacity-0 shadow-lg transition-all duration-300 ease-[cubic-bezier(0.34,1.56,0.64,1)] group-hover:translate-y-0 group-hover:scale-100 group-hover:opacity-100"
  style={{ bottom: `calc(${(Math.max(d.completed, d.added) / max) * 100}% + 8px)` }}
>
  <div className="mb-1 font-semibold">{d.day}</div>
  <div className="flex items-center gap-2 text-muted">
    <span className="h-2 w-2 rounded-full bg-accent" /> Completed
    <span className="ml-auto pl-3 text-fg">{d.completed}</span>
  </div>
  <div className="flex items-center gap-2 text-muted">
    <span className="h-2 w-2 rounded-full bg-accent/30" /> Added
    <span className="ml-auto pl-3 text-fg">{d.added}</span>
  </div>
</div>
              <div
                className="absolute bottom-0 w-2.5 rounded-full bg-accent"
                style={{ height: `${(d.completed / max) * 100}%` }}
              />
            </div>
            <span className="mt-3 text-xs text-muted">{d.day}</span>
          </div>
        ))}
      </div>
    </section>
  );
}