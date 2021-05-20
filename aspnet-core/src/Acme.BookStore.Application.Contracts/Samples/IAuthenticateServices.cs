using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Samples
{
    public interface IAuthenticateServices : IApplicationService
    {
        //Task<bool> Register(UserRegister model);
        Task<UserRegister> GetUserById(string id);
        Task<LoginResponse> Login(LoginDto model);
    }
}
