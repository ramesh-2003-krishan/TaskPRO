import { Binary } from "lucide-react";

export type ProfileDetail = {
    id : number;
    Name : string;
    Age : string;
    Email : string;
    University : string;
    Profile : string;
};

type props = {
    details : ProfileDetail[];
    onDelete : (id : number) => void;
    onEdit : (id : number) => void;
};

const RowHeaders = ["ID", "Name", "Age", "Email", "University", "Profile"];

export default function ProfileCard({details, onDelete, onEdit}:props){
    return(
        <div className="grid grid-cols-[repeat(auto-fit,minmax(260px,1fr))] gap-4">
  {details.length === 0 && (
    <p className="col-span-full py-10 text-center text-sm text-muted">No profiles yet.</p>
  )}

  {details.map((d) => {
    
    const values: Record<string, string | number> = {
      ID: d.id,
      Name: d.Name,
      Age: d.Age,
      Email: d.Email,
      University: d.University,
    };

    return (
      <div
        key={d.id}
        className="overflow-hidden rounded-xl border border-line bg-card transition-all duration-200 hover:-translate-y-1 hover:border-accent/40 hover:shadow-lg hover:shadow-accent/10"
      >
        
        {d.Profile ? (
          <img src={d.Profile} alt={d.Name} className="h-44 w-full object-cover" />
        ) : (
          <div className="flex h-44 w-full items-center justify-center bg-accent/15 font-display text-4xl font-bold text-accent">
            {d.Name.charAt(0).toUpperCase()}
          </div>
        )}

        
        <dl className="space-y-3 p-5">
          {RowHeaders.filter((h) => h !== "Profile").map((h) => (
            <div key={h} className="flex items-start justify-between gap-4 text-sm">
              <dt className="shrink-0 text-muted">{h}</dt>
              <dd className="min-w-0 truncate text-right font-medium" title={String(values[h])}>
                {values[h]}
              </dd>
            </div>
          ))}
        </dl>

       
        <div className="flex gap-2 border-t border-line p-4">
          <button
            onClick={() => onEdit(d.id)}
            className="flex-1 rounded-lg border border-line py-2 text-sm text-muted transition-colors hover:border-accent/40 hover:text-fg"
          >
            Edit
          </button>
          <button
            onClick={() => onDelete(d.id)}
            className="flex-1 rounded-lg border border-line py-2 text-sm text-danger transition-colors hover:bg-danger/10"
          >
            Delete
          </button>
        </div>
      </div>
    );
  })}
</div>
    );
}

