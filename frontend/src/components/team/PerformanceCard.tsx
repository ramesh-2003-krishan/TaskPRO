type props = {
    AssignedTasks : number;
    CompletedTask : number;
}

export default function PerformanceCard({AssignedTasks, CompletedTask}:props){
    const total = AssignedTasks;
    const PerformanceRate = total === 0 ? 0 : Math.round((CompletedTask/AssignedTasks)*100);

    return(
        <div>
            <h1>Performance</h1>
            <span>
                {PerformanceRate}
            </span>
        </div>
    )
}