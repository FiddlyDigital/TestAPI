using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Authentication.Configuration;
using TestAPI.Authentication.Models.DTO.Incoming;
using TestAPI.Authentication.Models.DTO.Outgoing;
using TestAPI.Authentication.Services.Interfaces;
using TestAPI.DAL.Configuration;

namespace TestAPI.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<UserRegistrationResponse> Register(UserRegistrationRequest registrationDTO)
        {
            var response = new UserRegistrationResponse();

            var userExists = await _userManager.FindByEmailAsync(registrationDTO.Email);
            if (userExists != null) // user email is already in the table
            {
                response.Success = false;
                response.Errors = new List<string> {
                    "Email Address already in use",
                };
                return response;
            }

            // add the user to the User Store
            var newUser = new IdentityUser()
            {
                Email = registrationDTO.Email,
                UserName = registrationDTO.Email,
                EmailConfirmed = true   // TODO: Add functionality to send EmailConfirmation to User 
            };

            var isCreated = await _userManager.CreateAsync(newUser, registrationDTO.Password);
            if (!isCreated.Succeeded)
            {
                response.Success = false;
                response.Errors = isCreated.Errors.Select(x => x.Description).ToList();
                return response;
            }

            await _unitOfWork.Users.Add(
                new DAL.Models.User()
                {
                    IdentityId = new Guid(newUser.Id),
                    Email = registrationDTO.Email,
                    FirstName = registrationDTO.FirstName,
                    LastName = registrationDTO.LastName,
                    DateOfBirth = new DateTime(),
                    Phone = string.Empty,
                    Country = string.Empty
                }
            );
            await _unitOfWork.CompleteAsync();

            // create a jwt token
            string token = GenerateJwtToken(newUser);

            response.Success = true;
            response.Token = token;          
            return response;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),                 // Sub is a unique ID - so any values is fine as long as it's unique
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  // Used by the refresh token
                }),
                Expires = DateTime.UtcNow.AddHours(3),                                  // TODO: Update to something like 5 mins later
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature                              // TODO: Review down the line
                )
            };

            // Generate SecurityToken
            var token = jwtHandler.CreateToken(tokenDescriptor);

            // Convert the SecurityToken to a string we can use
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
