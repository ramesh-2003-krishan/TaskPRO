"use client";

import { useState } from "react";

export type ProjectSlice = {
  name: string;
  value: number; 
  color: string; 
};

type Props = {
  data: ProjectSlice[];
  donut?: boolean; 
};

export default function ProjectPieCard({ data, donut = true }: Props) {
  const [active, setActive] = useState<number | null>(null);

  const total = data.reduce((sum, d) => sum + d.value, 0);

  
  const size = 200;
  const thickness = donut ? 26 : size / 2;
  const radius = (size - thickness) / 2 - 4;
  const circumference = 2 * Math.PI * radius;
  const gap = data.length > 1 ? 2 : 0;

 
  const lengthOf = (value: number) => (total === 0 ? 0 : (value / total) * circumference);

  const slices = data.map((d, i) => ({
    ...d,
    length: lengthOf(d.value),
    offset: data.slice(0, i).reduce((sum, prev) => sum + lengthOf(prev.value), 0),
    percent: total === 0 ? 0 : Math.round((d.value / total) * 100),
  }));

  const current = active === null ? null : slices[active];

  return (
    <section className="rounded-xl border border-line bg-card p-6 transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
      <h2 className="font-semibold">Tasks by Project</h2>
      <p className="mt-1 text-sm text-muted">Share of all work in each project</p>

      {total === 0 ? (
        <p className="py-10 text-center text-sm text-muted">No tasks yet.</p>
      ) : (
        <div className="mt-6 flex flex-col items-center gap-8 sm:flex-row">
          
          <div className="relative shrink-0" style={{ width: size, height: size }}>
            <svg
              width={size}
              height={size}
              viewBox={`0 0 ${size} ${size}`}
              className="-rotate-90"
              role="img"
              aria-label="Tasks by project"
            >
              {slices.map((s, i) => (
                <circle
                  key={s.name}
                  cx={size / 2}
                  cy={size / 2}
                  r={radius}
                  fill="none"
                  stroke={s.color}
                  strokeDasharray={`${Math.max(s.length - gap, 0)} ${circumference}`}
                  strokeDashoffset={-s.offset}
                  style={{
                    strokeWidth: active === i ? thickness + 8 : thickness,
                    opacity: active === null || active === i ? 1 : 0.35,
                  }}
                  className="cursor-pointer transition-all duration-200"
                  onMouseEnter={() => setActive(i)}
                  onMouseLeave={() => setActive(null)}
                />
              ))}
            </svg>

            
            {donut && (
              <div className="pointer-events-none absolute inset-0 flex flex-col items-center justify-center text-center">
                <span className="font-display text-3xl font-bold leading-none">
                  {current ? `${current.percent}%` : total}
                </span>
                <span className="mt-1.5 max-w-[110px] truncate text-xs text-muted">
                  {current ? current.name : "total tasks"}
                </span>
              </div>
            )}
          </div>

       
          <ul className="w-full flex-1 space-y-1">
            {slices.map((s, i) => (
              <li
                key={s.name}
                onMouseEnter={() => setActive(i)}
                onMouseLeave={() => setActive(null)}
                className={`flex cursor-default items-center gap-3 rounded-lg px-3 py-2 text-sm transition-colors ${
                  active === i ? "bg-white/5" : ""
                }`}
              >
                <span className="h-2.5 w-2.5 shrink-0 rounded-full" style={{ backgroundColor: s.color }} />
                <span className="flex-1 truncate">{s.name}</span>
                <span className="text-xs text-muted">{s.value} tasks</span>
                <span className="w-10 text-right font-semibold">{s.percent}%</span>
              </li>
            ))}
          </ul>
        </div>
      )}
    </section>
  );
}