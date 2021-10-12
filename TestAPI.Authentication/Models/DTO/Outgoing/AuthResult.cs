using System.Collections.Generic;

namespace TestAPI.Authentication.Models.DTO.Outgoing
{
    public class AuthResult
    {
        public List<string> Errors { get; set; }

        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
