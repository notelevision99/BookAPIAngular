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
    [Route("api/ProjectCompare/book")]
    public class BookController : ProjectCompareController
    {
        private readonly IBookServices _bookServices;
        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }
        
        [HttpGet]
        public async Task<JsonResult> GetBooks([FromQuery] int pageSize,int pageNumber, string? searchString)
        {
            var result = await _bookServices.GetBooks(pageSize,pageNumber,searchString);       
            return Json(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookServices.GetBookById(id);
            if(book == null)
            {
                return null;
            }
            return Ok(book);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            var result = await _bookServices.CreateBook(bookDto);
            if(result == 0)
            {
                return new BadRequestObjectResult(new { Message = "Update failed" });
            }   
            return Ok(new { Message = "Created Successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDto bookDto)
        {
            var result = await _bookServices.UpdateBook(id, bookDto);
            if(result == 0)
            {
                return new BadRequestObjectResult(new { Message = "Update failed" });
            }
            return Ok(new { Message = "Updated Successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await _bookServices.DeleteBook(id);
            if (result == 0)
            {
                return new BadRequestObjectResult(new { Message = "Delele Error" });
            }
            return Ok(new { Message = "Deleted Successfully" });

        }
    }
}
