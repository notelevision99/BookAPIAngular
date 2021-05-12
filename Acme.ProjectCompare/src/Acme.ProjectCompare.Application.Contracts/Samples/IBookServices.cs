using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.ProjectCompare.Samples
{
    public interface IBookServices : IApplicationService
    {
        Task<BookForList> GetBooks(int pageSize,int pageNumber,string searchString);
        Task<BookDto> GetBookById(Guid id);
        Task<int> UpdateBook(Guid id, BookDto bookDto);
        Task<int> DeleteBook(Guid id);

        Task<int> CreateBook(BookDto bookDto);
    }
}
