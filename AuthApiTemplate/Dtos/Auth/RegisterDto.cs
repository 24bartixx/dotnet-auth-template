using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiTemplate.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [MinLength(7)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{7,}$",
            ErrorMessage = "The password must have minimum 7 charackters, with uppercase letter, lowercase letter, digit and special character.")]
        public string Password { get; set; } = String.Empty;
        
        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}