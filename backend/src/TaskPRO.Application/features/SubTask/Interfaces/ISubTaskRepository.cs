using TaskPRO.Application.features.SubTask.DTOs;
using SubTaskEntity = TaskPRO.Domain.entities.SubTask;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.SubTask.Interfaces
{
    public interface ISubTaskRepository
    {
        Task<SubTaskEntity> CreateSubTaskAsync(SubTaskEntity subTask, Guid projectId, int taskItemId);
        Task<SubTaskEntity> UpdateSubTaskAsync(SubTaskEntity subTask, Guid projectId, int taskItemId);
    }
}