using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.features.Users.DTOs
{
    public class UpdateProfileRequest
    {
        [Required]
        public string Username { get; set; }=string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }=string.Empty;
        public string? phoneNumber { get; set; }=string.Empty;
    }
}