type Day = { day: string; completed: number; added: number };

type Props = {
  range: string;
  days: Day[]};


export default function WeeklyActivityCard({ range, days }: Props) {
  
  const max = Math.max(...days.map((d) => Math.max(d.completed, d.added)), 1);

  return (
    <section className="flex flex-col rounded-xl border border-line bg-card p-6">
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
          <div key={d.day} className="flex flex-1 flex-col items-center">
            <div className="relative flex w-full flex-1 items-end justify-center">
              
              <div
                className="absolute bottom-0 w-2.5 rounded-full bg-accent/30"
                style={{ height: `${(d.added / max) * 100}%` }}
                title={`${d.added} added`}
              />
              <div
                className="absolute bottom-0 w-2.5 rounded-full bg-accent"
                style={{ height: `${(d.completed / max) * 100}%` }}
                title={`${d.completed} completed`}
              />
            </div>
            <span className="mt-3 text-xs text-muted">{d.day}</span>
          </div>
        ))}
      </div>
    </section>
  );
}