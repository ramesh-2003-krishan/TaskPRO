using System;
using System.ComponentModel.DataAnnotations;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Users.DTOs
{
    public class UserLoginRequest
    {
        [Required]
        public string CurrentPassword {get; set;} = string.Empty;

        [Required]
        public string UserEmail {get; set;} =  string.Empty;
    }
}