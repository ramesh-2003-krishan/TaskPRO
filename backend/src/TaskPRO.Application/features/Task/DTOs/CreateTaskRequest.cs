using System;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Task
{
    public class CreateTaskRequest
    {
        public string Title { get; set;}= string.Empty;
        public string Description { get; set;} = string.Empty;
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));
        public Guid? AssignedToUserId { get; set; }
         public TaskPRO.Domain.enums.Priority Priority { get; set; }= TaskPRO.Domain.enums.Priority.Low;
        
   }
}