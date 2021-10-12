using System.ComponentModel.DataAnnotations;

namespace TestAPI.Authentication.Models.DTO.Incoming
{
    public class UserRegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
