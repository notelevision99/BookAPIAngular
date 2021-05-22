using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Samples
{
    public interface IAuthenticateServices : IApplicationService
    {

        Task<(LoginResponse, int)> Login(LoginDto model);
    }
}
