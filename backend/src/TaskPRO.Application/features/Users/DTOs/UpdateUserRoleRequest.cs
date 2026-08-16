using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.features.Users.DTOs
{
    public class UpdateUserRoleRequest
    {
        [Required]
        public string Role { get; set; }=string.Empty;
    }
}