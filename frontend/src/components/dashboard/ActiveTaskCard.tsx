type ActiveTask = {
    task: string;
    date: string;
    status: string;
    profile: string;
};

export default function ActiveTaskCard({ task, date, status, profile }: ActiveTask) {
  return (
    <div className="flex items-center gap-4 rounded-lg border border-line bg-card p-4">
        <div className="flex h-10 w-10 items-center justify-center rounded-full bg-accent/10">
            <img src={profile} alt="Profile" className="h-6 w-6 rounded-full" />
        </div>
        <div className="flex-1">
            <h3 className="font-semibold">{task}</h3>
            <p className="text-sm text-muted">{date}</p>
        </div>
        <span className={`inline-flex items-center rounded-full px-3 py-1 text-xs font-medium ${
            status === "In Progress" ? "bg-blue-100 text-blue-800" :
            status === "In Review" ? "bg-yellow-100 text-yellow-800" :
            "bg-green-100 text-green-800"
        }`}>
            {status}
        </span>
        </div>
);
}