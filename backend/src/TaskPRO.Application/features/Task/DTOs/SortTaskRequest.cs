namespace TaskPRO.Application.features.Task.DTOs
{
    public enum SortOrder
    {
        Asc,
        Desc
    }

    public class SortTaskRequest
    {
        public string? SortBy { get; set; }
        public SortOrder? SortOrder { get; set; } = global::TaskPRO.Application.features.Task.DTOs.SortOrder.Asc;
    }
}