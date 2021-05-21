using Acme.ProjectCompare.Samples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Controllers
{
    [Authorize]
    [RemoteService]
    [Route("/api/books")]
    public class BookController : AbpController
    {
        private readonly IBookServices _bookServices;
        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet]
        public async Task<JsonResult> GetBooks([FromQuery] int pageSize, int pageNumber, string searchString)
        {
            var result = await _bookServices.GetBooks(pageSize, pageNumber, searchString);
            return Json(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookServices.GetBookById(id);
            if (book == null)
            {
                return new BadRequestObjectResult(new { Message = "Cannot find book!" });
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            var result = await _bookServices.CreateBook(bookDto);
            if (result == 0)
            {
                return new BadRequestObjectResult(new { Message = "Book Name is  not null" });
            }
            if(result == -1)
            {
                return new BadRequestObjectResult(new { Message = "Book Type is not null" });
            }
            if(result == -2)
            {
                return new BadRequestObjectResult(new { Message = "Book Description is not null" });
            }
            return Ok(new { Message = "Created Successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDto bookDto)
        {
            var result = await _bookServices.UpdateBook(id, bookDto);
            if (result == false)
            {
                return new BadRequestObjectResult(new { Message = "Update failed" });
            }
            return Ok(new { Message = "Updated Successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await _bookServices.DeleteBook(id);
            if (result == false)
            {
                return new BadRequestObjectResult(new { Message = "Delele Error" });
            }
            return Ok(new { Message = "Deleted Successfully" });
        }
    }
}

