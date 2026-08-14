using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.DTOs.RefreshTokenRequest
{
  public class RefreshTokenRequest
  {
   [Required]
   public string RefreshToken { get; set; } = string.Empty;
  }
}