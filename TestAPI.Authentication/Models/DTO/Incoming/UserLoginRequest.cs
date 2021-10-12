using System.ComponentModel.DataAnnotations;

namespace TestAPI.Authentication.Models.DTO.Incoming
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
