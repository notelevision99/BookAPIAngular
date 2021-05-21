
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Services;

    namespace Acme.ProjectCompare.Samples
    {
        public interface IBookServices : IApplicationService
        {
            Task<BookList> GetBooks(int pageSize, int pageNumber, string searchString);
            Task<BookDto> GetBookById(Guid id);
            Task<bool> UpdateBook(Guid id, BookDto bookDto);
            Task<bool> CreateBook(BookDto bookDto);
            Task<bool> DeleteBook(Guid id);
        }
    }
