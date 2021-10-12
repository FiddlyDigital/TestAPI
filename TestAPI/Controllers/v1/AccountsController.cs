using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI.Authentication.Models.DTO.Incoming;
using TestAPI.Authentication.Models.DTO.Outgoing;
using TestAPI.Authentication.Services.Interfaces;
using TestAPI.DAL.Configuration;

namespace TestAPI.Controllers.v1
{
    public class AccountsController : BaseController
    {
        private readonly IAuthenticationService _authService;

        public AccountsController (
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork,
            IAuthenticationService authService) : base(logger, unitOfWork)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest registrationDTO)
        {
            if (ModelState.IsValid)
            {
                var registrationResponse = await _authService.Register(registrationDTO);
                if (!registrationResponse.Success)
                {
                    return BadRequest(registrationResponse);
                }

                return Ok(registrationResponse);
            }

            return BadRequest(new UserRegistrationResponse()
            {
                Success = false,
                Errors = new List<string> {
                    "Invalid payload for UserRegistration" 
                }
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginDTO)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _authService.Login(loginDTO);

                if (!loginResponse.Success)
                {
                    return BadRequest(loginResponse);
                }

                return Ok(loginResponse);
            }

            return BadRequest();
        }

        //public async Task<IActionResult> ConfirmEmail([FromBody]
    }
}
