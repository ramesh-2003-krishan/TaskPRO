using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public TaskPRO.Domain.enums.TaskStatus Status { get; set; }= TaskPRO.Domain.enums.TaskStatus.NotStarted;
    }
}