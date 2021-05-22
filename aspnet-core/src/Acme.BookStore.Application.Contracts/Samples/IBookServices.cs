
using Acme.BookStore.Samples;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.ProjectCompare.Samples
{
    public interface IBookServices : IApplicationService
    {
        Task<BookList> GetBooks(FilterDto filterDto);
        Task<BookDto> GetBookById(Guid id);
        Task<bool> UpdateBook(Guid id, BookDto bookDto);
        Task<int> CreateBook(BookDto bookDto);
        Task<bool> DeleteBook(Guid id);
    }
}
