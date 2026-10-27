namespace TaskPRO.Application.common.Options
{
    public class FileStorageOption
    {
        public string RootPath { get; set; } ="uploads";
        public string MaxFileSizeBytes { get; set; } = "10485760"; 
        public string AllowedExtensions { get; set; } = ".jpg,.jpeg,.png,.gif,.pdf,.docx,.xlsx";
    }
}