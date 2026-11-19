type props = {
    backlog: number;
    inProgress: number;
    inReview: number;
    done: number;
};

export default function ProgressCards({ backlog, inProgress, inReview, done }: props) {
    const total = backlog + inProgress + inReview + done;
    const percentBacklog = total === 0 ? 0 : Math.round((backlog / total) * 100);
    const percentInProgress = total === 0 ? 0 : Math.round((inProgress / total)*100);
    const percentInReview = total === 0 ? 0 : Math.round((inReview / total)*100);
    const percentdone = total === 0 ? 0 : Math.round((done / total)*100);

 
  const size = 108;
  const stroke = 10;
  const radius = (size - stroke) / 2;
  const circumference = 2 * Math.PI * radius;
  const filled = (percentBacklog / 100) * circumference;
  const filledInProgress = (percentInProgress / 100) * circumference;
  const filledInReview = (percentInReview / 100) * circumference;
  const filledDone = (percentdone / 100) * circumference;

  const rows = [
    { label: "Backlog", value: backlog, color: "bg-[#5b5b78]" },
    { label: "In Progress", value: inProgress, color: "bg-accent" },
    { label: "In Review", value: inReview, color: "bg-warn" },
    { label: "Done", value: done, color: "bg-ok" },
  ];

  return (
    <div className=" grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4 rounded-xl p-5 transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
        <section className="rounded-xl border border-line bg-card p-6 transition-transform duration-200 ease-out hover:scale-[1.1] hover:shadow-lg hover:shadow-accent/10">
      <h2 className="font-semibold">Backlog Progress</h2>

      
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
              strokeDasharray={`${filledInProgress} ${circumference}`}
              className="stroke-accent"
            />
          </svg>
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <span className="font-display text-2xl font-bold leading-none">{percentBacklog}%</span>
            <span className="mt-1 text-[11px] text-muted">complete</span>
          </div>
        </div>
      </div>

    </section>

    <section className="rounded-xl border border-line bg-card p-6 transition-transform duration-200 ease-out hover:scale-[1.1] hover:shadow-lg hover:shadow-accent/10">
      <h2 className="font-semibold">InProgress Progress</h2>

      
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
            <span className="font-display text-2xl font-bold leading-none">{percentInProgress}%</span>
            <span className="mt-1 text-[11px] text-muted">complete</span>
          </div>
        </div>
      </div>

    </section>

    <section className="rounded-xl border border-line bg-card p-6 transition-transform duration-200 ease-out hover:scale-[1.1] hover:shadow-lg hover:shadow-accent/10">
      <h2 className="font-semibold">InReview Progress</h2>

      
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
              strokeDasharray={`${filledInReview} ${circumference}`}
              className="stroke-accent"
            />
          </svg>
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <span className="font-display text-2xl font-bold leading-none">{percentInReview}%</span>
            <span className="mt-1 text-[11px] text-muted">complete</span>
          </div>
        </div>
      </div>

    </section>

    <section className="rounded-xl border border-line bg-card p-6 transition-transform duration-200 ease-out hover:scale-[1.1] hover:shadow-lg hover:shadow-accent/10">
      <h2 className="font-semibold">Done Progress</h2>

      
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
              strokeDasharray={`${filledDone} ${circumference}`}
              className="stroke-accent"
            />
          </svg>
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <span className="font-display text-2xl font-bold leading-none">{percentdone}%</span>
            <span className="mt-1 text-[11px] text-muted">complete</span>
          </div>
        </div>
      </div>

    </section>


    </div>
    
    
  );
}