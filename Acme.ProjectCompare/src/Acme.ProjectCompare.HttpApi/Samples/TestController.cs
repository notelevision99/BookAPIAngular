using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.ProjectCompare.Samples
{
    [RemoteService]
    [Route("/api/test")]
    public class TestController : ProjectCompareController
    {
        private readonly IAuthenticateServices _authService;
        public TestController(IAuthenticateServices authService)
        {
            _authService = authService;
        }
        //[HttpPost]
        //public async Task<IActionResult> GetBookById([FromBody] UserRegister model)
        //{
        //    var book = await _authService.Register(model);
        //    if (book == null)
        //    {
        //        return new BadRequestObjectResult(new { Message = "Cannot find book!" });
        //    }
        //    return Ok(book);
        //}


    }
}
