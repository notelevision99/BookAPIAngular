using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Acme.ProjectCompare.Samples
{
    public interface IAuthenticateServices : IApplicationService
    {
        Task<bool> Register(UserRegister model);
        Task<UserRegister> GetUserById(Guid id);
    }
}
