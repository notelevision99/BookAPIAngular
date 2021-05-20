using Acme.BookStore.Samples;
using Acme.BookStore.Users;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore.NewFolder
{
  
    public class AuthenticateServices : ApplicationService, IAuthenticateServices
    {
        private readonly IdentityUserManager _userManager;
        public AuthenticateServices(IdentityUserManager userManager
            )
        {
            _userManager = userManager;
        }
        public async Task<UserRegister> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<LoginResponse> Login(LoginDto model)
        {
            const string validIssuer = "https://localhost:44313";
            const string validAudience = "http://localhost:61955";
            const string secretKey = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var token = new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                expires: DateTime.Now.AddHours(0.01),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
