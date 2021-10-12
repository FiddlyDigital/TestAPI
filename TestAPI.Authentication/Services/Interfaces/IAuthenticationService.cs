using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Authentication.Models.DTO.Incoming;
using TestAPI.Authentication.Models.DTO.Outgoing;

namespace TestAPI.Authentication.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserRegistrationResponse> Register(UserRegistrationRequest registrationDTO);
    }
}
