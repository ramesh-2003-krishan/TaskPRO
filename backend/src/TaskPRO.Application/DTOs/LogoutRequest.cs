using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.DTOs.LogoutRequest
{
    public class LogoutRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}