using Acme.BookStore.Samples;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Controllers
{
    [RemoteService]
    [Route("/api/auth")]
    public class AuthController : AbpController
    {
        private readonly IAuthenticateServices _authServices;
        public AuthController(IAuthenticateServices authServices)
        {
            _authServices = authServices;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authServices.Login(model);
            if (result == (null,-1))
            {
                return new BadRequestObjectResult(new { Message = "Username and password must be filled" });
            }
            if (result == (null, 0))
            {
                return new BadRequestObjectResult(new { Message = "Account not exist" });
            }
            return Ok(result.Item1);
        }
    }
}
