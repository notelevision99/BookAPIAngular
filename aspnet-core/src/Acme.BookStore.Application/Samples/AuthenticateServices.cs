using Acme.BookStore.Samples;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Acme.BookStore.NewFolder
{
    public class AuthenticateServices : ApplicationService, IAuthenticateServices
    {
        private readonly IdentityUserManager _userManager;
        private IConfiguration _configuration;
        public AuthenticateServices(IdentityUserManager userManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(LoginResponse,int)> Login(LoginDto model)
        {          
            /* Validate User
             * 0 : Pwd or Username incorrect => Account not exist
             * -1 : Username or Password null
             */
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password)) { return (null,-1); }  
            if(user == null) { return (null, 0); }
            if(user.UserName != model.UserName) { return (null,0); }
            var checkPwd = (_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Failed) ? true : false;
            if(!checkPwd) { return (null,0); }
            /*
             */

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return (new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                UserName = user.UserName,
                Email = user.Email
            },1);
        }
    }
}
