using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.DTOs.RegisterRequest
{
  public class RegisterRequest
  {
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
  }
}