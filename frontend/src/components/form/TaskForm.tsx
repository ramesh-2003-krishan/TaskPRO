"use client";

import { useEffect, useState, ChangeEvent, FormEvent } from "react";
import { FileQuestionMark } from "lucide-react";
import { Priority } from "../board/TaskCard";

interface FormData{
    Project : string;
    Task : string;
    Priority : Priority;
    DueDate : string;
    Assignee : string;
}

type FormErrors = Partial<Record<keyof FormData, string>>;

function validate (data: FormData): FormErrors{
    const err: FormErrors={};
    if(!data.Project.trim()) err.Project = "Required";
    if(!data.Task.trim()) err.Task = "Required";
    if(!data.Priority.trim()) err.Priority ="Required";
    if(!data.DueDate.toString()) err.DueDate ="Required";
    return err;
}

const inputCls = (error?: string) =>
  `w-full border rounded-xl px-3 py-2.5 text-sm outline-none transition ${
    error
      ? "border-red-400 focus:ring-1 focus:ring-red-400"
      : "border-gray-200 focus:border-teal-500 focus:ring-1 focus:ring-teal-500"
  }`;

const INITIAL: FormData={
    Project:"",
    Task:"",
    Priority:"Low",
    DueDate:"",
    Assignee:""
}

export default function FormData(){
    const[data, setData] = useState<FormData>(INITIAL);
    const[error,setError] = useState<FormErrors>({});
    const[done,setDone] = useState(false);

    const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target;
    const checked = (e.target as HTMLInputElement).checked;
    setData((p) => ({
      ...p,
      [name]: type === "checkbox" ? checked : value,
    }));
    if (error[name as keyof FormData]) {
      setError((p) => ({ ...p, [name]: undefined }));
    }
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const errs = validate(data);
    if (Object.keys(errs).length > 0) { setError(errs); return; }
    console.log("Submitted:", data);
    setDone(true);
  };
   if (done) {
    return (
      <div className="max-w-lg mx-auto p-10 text-center flex flex-col items-center gap-4">
        <div className="w-14 h-14 bg-teal-500 rounded-full flex items-center justify-center">
          <svg className="w-7 h-7 text-white" fill="none" stroke="currentColor" strokeWidth={2.5} viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" d="M5 13l4 4L19 7" />
          </svg>
        </div>
        <h2 className="text-xl font-bold text-gray-800">Welcome, {data.Project}!</h2>
        <p className="text-sm text-gray-500">Your account has been created.</p>
      </div>
    );
  }
  return(
    <form
  onSubmit={handleSubmit}
  noValidate
  className="mx-auto w-full max-w-lg space-y-5 rounded-2xl border border-line bg-card p-6"
>
 
  <div>
    <h2 className="font-display text-xl font-bold">Create a new task</h2>
    <p className="mt-1 text-sm text-muted">Fill in the details and assign it to your team.</p>
  </div>

 
  <div>
    <label htmlFor="Project" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
      Project
    </label>
    <select
      id="Project"
      name="Project"
      value={data.Project}
      onChange={handleChange}
      className={`${inputCls(error.Project)} bg-card text-fg`}
    >
      <option value="" className="bg-card">Select a project</option>
      {["Platform Redesign", "Mobile App", "API Integration", "Design System"].map((p) => (
        <option key={p} value={p} className="bg-card">{p}</option>
      ))}
    </select>
    {error.Project && <p className="mt-1 text-xs text-danger">{error.Project}</p>}
  </div>

  
  <div>
    <label htmlFor="Task" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
      Task
    </label>
    <input
      id="Task"
      name="Task"
      type="text"
      value={data.Task}
      onChange={handleChange}
      placeholder="e.g. Fix crash on iOS 17 deep links"
      className={`${inputCls(error.Task)} bg-card text-fg placeholder:text-muted`}
    />
    {error.Task && <p className="mt-1 text-xs text-danger">{error.Task}</p>}
  </div>

 
  <div>
    <span className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">Priority</span>
    <div className="grid grid-cols-3 gap-2">
      {(
        [
          { value: "Low", dot: "bg-ok", on: "peer-checked:border-ok peer-checked:bg-ok/15 peer-checked:text-ok" },
          { value: "Medium", dot: "bg-warn", on: "peer-checked:border-warn peer-checked:bg-warn/15 peer-checked:text-warn" },
          { value: "High", dot: "bg-danger", on: "peer-checked:border-danger peer-checked:bg-danger/15 peer-checked:text-danger" },
        ] as { value: Priority; dot: string; on: string }[]
      ).map((p) => (
        <label key={p.value} className="cursor-pointer">
          <input
            type="radio"
            name="Priority"
            value={p.value}
            checked={data.Priority === p.value}
            onChange={handleChange}
            className="peer sr-only"
          />
          <span
            className={`flex items-center justify-center gap-2 rounded-xl border border-line py-2.5 text-sm text-muted transition hover:text-fg peer-focus-visible:ring-1 peer-focus-visible:ring-accent ${p.on}`}
          >
            <span className={`h-2 w-2 rounded-full ${p.dot}`} />
            {p.value}
          </span>
        </label>
      ))}
    </div>
    {error.Priority && <p className="mt-1 text-xs text-danger">{error.Priority}</p>}
  </div>

  <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
    <div>
      <label htmlFor="DueDate" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
        Due date
      </label>
      <input
        id="DueDate"
        name="DueDate"
        type="date"
        value={data.DueDate}
        onChange={handleChange}
        className={`${inputCls(error.DueDate)} bg-card text-fg [color-scheme:dark]`}
      />
      {error.DueDate && <p className="mt-1 text-xs text-danger">{error.DueDate}</p>}
    </div>

    <div>
      <label htmlFor="Assignee" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
        Assignee
      </label>
      <select
        id="Assignee"
        name="Assignee"
        value={data.Assignee}
        onChange={handleChange}
        className={`${inputCls(error.Assignee)} bg-card text-fg`}
      >
        <option value="" className="bg-card">Unassigned</option>
        {["Alex Park", "Jamie Lee", "Morgan Chen", "Sam Rivera", "Riley Kim"].map((n) => (
          <option key={n} value={n} className="bg-card">{n}</option>
        ))}
      </select>
    </div>
  </div>

  
  <div className="flex items-center justify-end gap-3 pt-2">
    <button
      type="button"
      onClick={() => { setData(INITIAL); setError({}); }}
      className="rounded-xl px-4 py-2.5 text-sm text-muted transition-colors hover:bg-white/5 hover:text-fg"
    >
      Reset
    </button>
    <button
      type="submit"
      className="rounded-xl bg-accent px-5 py-2.5 text-sm font-medium text-white transition hover:brightness-110"
    >
      Create Task
    </button>
  </div>
</form>
  );
  
}

