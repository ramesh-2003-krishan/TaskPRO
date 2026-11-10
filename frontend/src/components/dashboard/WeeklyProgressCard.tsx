type props = {
    backlog: number;
    inProgress: number;
    inReview: number;
    done: number;
};

export default function WeeklyProgressCard({ backlog, inProgress, inReview, done }: props) {
    const total = backlog + inProgress + inReview + done;
    const percent = total === 0 ? 0 : Math.round((done / total) * 100);

 
  const size = 108;
  const stroke = 10;
  const radius = (size - stroke) / 2;
  const circumference = 2 * Math.PI * radius;
  const filled = (percent / 100) * circumference;

  const rows = [
    { label: "Backlog", value: backlog, color: "bg-[#5b5b78]" },
    { label: "In Progress", value: inProgress, color: "bg-accent" },
    { label: "In Review", value: inReview, color: "bg-warn" },
    { label: "Done", value: done, color: "bg-ok" },
  ];

  return (
    <section className="rounded-xl border border-line bg-card p-6">
      <h2 className="font-semibold">Progress</h2>

      
      <div className="mt-5 flex justify-center">
        <div className="relative" style={{ width: size, height: size }}>
          <svg width={size} height={size} className="-rotate-90">
            <circle
              cx={size / 2}
              cy={size / 2}
              r={radius}
              fill="none"
              strokeWidth={stroke}
              className="stroke-line"
            />
            <circle
              cx={size / 2}
              cy={size / 2}
              r={radius}
              fill="none"
              strokeWidth={stroke}
              strokeLinecap="round"
              strokeDasharray={`${filled} ${circumference}`}
              className="stroke-accent"
            />
          </svg>
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <span className="font-display text-2xl font-bold leading-none">{percent}%</span>
            <span className="mt-1 text-[11px] text-muted">complete</span>
          </div>
        </div>
      </div>

      
      <ul className="mt-6 space-y-3.5">
        {rows.map((r) => (
          <li key={r.label} className="flex items-center gap-3 text-sm">
            <span className="w-19 shrink-0 text-muted">{r.label}</span>
            <div className="h-1.5 flex-1 overflow-hidden rounded-full bg-line">
              <div
                className={`h-full rounded-full ${r.color}`}
                style={{ width: `${total === 0 ? 0 : (r.value / total) * 100}%` }}
              />
            </div>
            <span className="w-4 text-right text-xs text-muted">{r.value}</span>
          </li>
        ))}
      </ul>
    </section>
  );
}