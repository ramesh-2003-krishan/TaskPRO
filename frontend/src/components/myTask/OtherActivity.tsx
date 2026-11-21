export type props = {
    Activity: string;
    Date: string;
    Time: string;
}

const headers = ["Activity","Date","Time"];

export default function OtherActivity({ activities }: { activities: props[] }) {
    return(
        <div className="overflow-hidden rounded-xl border border-line bg-card transition-all hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10">
            
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
        </div>
    )
}