using Acme.ProjectCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Acme.ProjectCompare.Samples
{
    public class AuthenticateServices : ApplicationService, ITransientDependency, IAuthenticateServices
    {
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        private readonly IdentityUserManager _userManager;
        public AuthenticateServices(IRepository<IdentityUser, Guid> userRepository, IdentityUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<bool> Register(UserRegister model)
        {
            var userExists = await _userRepository.FirstOrDefaultAsync(p => p.UserName == model.UserName);
            if (userExists != null)
                return false;
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,            
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return true;
        }

        public async Task<UserRegister> GetUserById(Guid id)
        {
            var userResult = await _userRepository.GetAsync(p => p.Id == id);
            return new UserRegister { 
                UserName = userResult.UserName,
                Email = userResult.Email
            };
        }

        
    }


}

