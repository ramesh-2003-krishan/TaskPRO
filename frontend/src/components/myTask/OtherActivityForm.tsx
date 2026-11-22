"use client";

import { useEffect, useState, ChangeEvent, FormEvent,useRef } from "react";


interface FormData{
    Activity : string;
    Date : string;
    Time : string;
}

type FormErrors = Partial<Record<keyof FormData, string>>;

function validate (data: FormData): FormErrors{
    const err: FormErrors={};
    if(!data.Activity.trim()) err.Activity = "Required";
    if(!data.Date.toString()) err.Date ="Required";
    if(!data.Time.toString()) err.Time ="Required";
    return err;
}

const inputCls = (error?: string) =>
  `w-full border rounded-xl px-3 py-2.5 text-sm outline-none transition ${
    error
      ? "border-red-400 focus:ring-1 focus:ring-red-400"
      : "border-gray-200 focus:border-teal-500 focus:ring-1 focus:ring-teal-500"
  }`;

const INITIAL: FormData={
    Activity:"",
    Date:"",
    Time:""
}

export default function OtherActivityForm(){
    const[data, setData] = useState<FormData>(INITIAL);
    const[error,setError] = useState<FormErrors>({});
    const[done,setDone] = useState(false);
     const[formOpen, setFormOpen] = useState(false);
     const formRef = useRef<HTMLDivElement>(null);

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


    useEffect(()=>{
      if(!formOpen) return;

      const handlePointerDown = (event: PointerEvent)=>{
        if(event.target instanceof Node && !formRef.current?.contains(event.target)){
          setFormOpen(false);
        }
      };

      document.addEventListener("pointerdown", handlePointerDown);
      return ()=> document.removeEventListener("pointerdown", handlePointerDown);
    },[formOpen])
   if (done) {
    return (
      <div className="max-w-lg mx-auto p-10 text-center flex flex-col items-center gap-4">
        <div className="w-14 h-14 bg-teal-500 rounded-full flex items-center justify-center">
          <svg className="w-7 h-7 text-white" fill="none" stroke="currentColor" strokeWidth={2.5} viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" d="M5 13l4 4L19 7" />
          </svg>
        </div>
        <h2 className="text-xl font-bold text-gray-800">Welcome, {data.Activity}!</h2>
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
    <h2 className="font-display text-xl font-bold">Add new Activity</h2>
    <p className="mt-1 text-sm text-muted">Fill in the details and assign it to your team.</p>
  </div>

 
  <div>
    <label htmlFor="Project" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
      Activity
    </label>
    <input
      id="Task"
      name="Task"
      type="text"
      value={data.Activity}
      onChange={handleChange}
      placeholder="e.g. Fix crash on iOS 17 deep links"
      className={`${inputCls(error.Activity)} bg-card text-fg placeholder:text-muted`}
    />
    {error.Activity && <p className="mt-1 text-xs text-danger">{error.Activity}</p>}
  </div>

  <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
    <div>
      <label htmlFor="DueDate" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
        Due date
      </label>
      <input
        id="Date"
        name="Date"
        type="date"
        value={data.Date}
        onChange={handleChange}
        className={`${inputCls(error.Date)} bg-card text-fg scheme-dark`}
      />
      {error.Date && <p className="mt-1 text-xs text-danger">{error.Date}</p>}
    </div>

    <div>
      <label htmlFor="Assignee" className="mb-1.5 block text-xs font-semibold uppercase tracking-wider text-muted">
        Time
      </label>
      <input
      id="Task"
      name="Task"
      type="text"
      value={data.Time}
      onChange={handleChange}
      placeholder="e.g. Fix crash on iOS 17 deep links"
      className={`${inputCls(error.Time)} bg-card text-fg placeholder:text-muted`}
    />
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
      Create Activity
    </button>
  </div>
</form>
  );
  
}

