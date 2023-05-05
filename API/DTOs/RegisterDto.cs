

using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    // DTO > Data Transfer Object
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}