import TaskCard, { type BoardTask } from "@/components/board/TaskCard";

export type priority = "Low";

export type prior= {
    priority: priority;
}

const priorityStyle: Record<priority, { badge: string; dot: string }> = {
  Low: { badge: "bg-ok/15 text-ok", dot: "bg-ok" },
};

const sample: BoardTask[] = [
{
  id: 1,
  title: "Fix crash on iOS 17 deep links",
  tags: ["Bug", "iOS"],
  priority: "High",
  date: "Aug 9",
  assignee: { initials: "SR", color: "bg-ok" },
},
{
     id: 1,
  title: "Fix button UI",
  tags: ["Bug", "iOS"],
  priority: "Low",
  date: "Aug 9",
  assignee: { initials: "SR", color: "bg-ok" },
},
];



export default function BoardPage({priority}:prior) {
    const p = priorityStyle[priority] ?? {badge:"",dot:""};
  return (
    <section className="flex items-start gap-6 overflow-x-auto pb-4">
    <div className="max-w-[320px] shrink-0">
      <div className="flex items-center gap-3">
          <span
            className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-semibold ${p.badge}`}
          >
            <span className={`h-2 w-2 rounded-full bg-ok`} />
          </span>
          <h1>Backlog</h1>
        </div>
        <br />
       <div className="flex flex-col gap-2">
         {sample.map((task)=>(
        <TaskCard key={task.id} {...task}/>
          ))}
        </div>
   
    </div>
    <div className="max-w-[320px] shrink-0" >
      <div className="flex items-center gap-3">
          <span
            className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-semibold ${p.badge}`}
          >
            <span className={`h-2 w-2 rounded-full bg-warn`} />
          </span>
          <h1>InProgress</h1>
        </div>
        <br />
       <div className="flex flex-col gap-2">
         {sample.map((task)=>(
        <TaskCard key={task.id} {...task}/>
          ))}
        </div>
   
    </div>

    <div className="max-w-[320px] shrink-0" >
      <div className="flex items-center gap-3">
          <span
            className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-semibold ${p.badge}`}
          >
            <span className={`h-2 w-2 rounded-full bg-fuchsia-700`} />
          </span>
          <h1>In Review</h1>
        </div>
        <br />
       <div className="flex flex-col gap-2">
         {sample.map((task)=>(
        <TaskCard key={task.id} {...task}/>
          ))}
        </div>
   
    </div>

    <div className="max-w-[320px] shrink-0" >
      <div className="flex items-center gap-3">
          <span
            className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-semibold ${p.badge}`}
          >
            <span className={`h-2 w-2 rounded-full bg-red-700`} />
          </span>
          <h1>Done</h1>
        </div>
        <br />
       <div className="flex flex-col gap-2">
         {sample.map((task)=>(
        <TaskCard key={task.id} {...task}/>
          ))}
        </div>
   
    </div>
    </section>
  );
}