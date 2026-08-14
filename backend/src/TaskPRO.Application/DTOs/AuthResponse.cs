using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.DTOs.AuthResponse
{
  public class AuthResponse
  {
    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string RefreshToken { get; set; } = string.Empty;
  }
}