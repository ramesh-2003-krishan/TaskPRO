using TaskPRO.Domain.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Task.DTOs
{
    public class UnAssignTaskRequest
    {
        [Required]
        public int TaskId { get; set; }
    }
}