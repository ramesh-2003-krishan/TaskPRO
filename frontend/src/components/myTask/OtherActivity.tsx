import { Plus } from "lucide-react";
import { useState,useRef,useEffect } from "react";
import OtherActivityForm from "./OtherActivityForm";


export type props = {
    Activity: string;
    Date: string;
    Time: string;
}

const headers = ["Activity","Date","Time"];

export default function OtherActivity({ activities }: { activities: props[] }) {
    const[formOpen, setFormOpen] = useState(false);
    const formRef = useRef<HTMLDivElement>(null);

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
    
    return(
        <section className="overflow-hidden rounded-xl border border-line bg-card transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
            <div className="flex items-start justify-between px-5 pt-5 pb-3 font-semibold">
              <h2 className=" font-semibold">Other Activity</h2>
<div ref={formRef} className="relative">
           <button 
           aria-label="form"
           aria-expanded={formOpen}
           className="flex h-9 items-center gap-2 rounded-lg bg-accent px-4 text-sm font-medium text-white hover:brightness-110"
           onClick={
            ()=>setFormOpen((open)=>!open)
           }
           >
          <Plus size={15} />
          Add Activity
        </button>
        {formOpen && (
           <div className="absolute right-0 top-full z-50 mt-2 w-[min(24rem,calc(100vw-2rem))]">
              <OtherActivityForm/>
            </div>
        )}
        </div>
            </div>
            
            <div className="overflow-x-auto">
                
                <table className="w-full min-w-215 text-left">
                    <thead>
                      <tr className="border-b border-line">
                      {headers.map((h) => (
                      <th key={h} className="px-4 py-4 text-[11px] font-semibold tracking-wider text-muted first:pl-5">
                        {h}
                      </th>
                      ))}
                      </tr>
                    </thead>
                    <tbody>
                        {activities.map((a, i) => (
                           <tr key={i} className="group border-b border-line last:border-b-0 transition-colors hover:bg-white/3">
                               <td className="py-3.5 pl-5 pr-4 text-[15px]">{a.Activity}</td>
                               <td className="px-4 py-3.5 text-sm text-muted">{a.Date}</td>
                               <td className="px-4 py-3.5 text-sm text-muted">{a.Time}</td>
                        </tr>
                         ))}
                    </tbody>
                </table>
            </div>
        </section>
    )
}