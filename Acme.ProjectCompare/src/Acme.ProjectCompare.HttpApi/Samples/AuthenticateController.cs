using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.ProjectCompare.Samples
{

    [Route("api/auth")]
    public class AuthenticateController : ProjectCompareController
    {
        private readonly IAuthenticateServices _authService;
        public AuthenticateController(IAuthenticateServices authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var result = await _authService.Register(userRegister);
            if(result == false)
            {
                return new BadRequestObjectResult(new { Message = "Failed" });
            }
            return Ok(result);
        }   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _authService.GetUserById(id);
            return Ok(result);
        }
    }
}
