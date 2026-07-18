using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class SubTaskPriority
    {
        public int Id {get; set;}
        public string Name {get; set;}= string.Empty;

        public TaskPRO.Domain.enums.Priority Priority {get; set;}= TaskPRO.Domain.enums.Priority.Low;
    }
}