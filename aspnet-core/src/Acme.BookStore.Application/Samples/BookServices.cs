using Volo.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Acme.BookStore;

namespace Acme.ProjectCompare.Samples
{
    public class BookServices : ApplicationService, IBookServices
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        private readonly IObjectMapper<BookStoreApplicationModule> _bookMapper;

        public BookServices(IRepository<Book, Guid> bookRepository, IObjectMapper<BookStoreApplicationModule> bookMapper)
        {
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
        }
        public async Task<BookList> GetBooks(int pageSize, int pageNumber, string searchString)
        {
            var source = (string.IsNullOrWhiteSpace(searchString))
                ? _bookRepository
                : _bookRepository.Where(b =>
                  b.BookName.Contains(searchString) || b.BookType.Contains(searchString) || b.Description.Contains(searchString)
                ).AsQueryable();

            var totalCount = source.Count();
            var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            var previousPage = pageNumber > 1 ? (pageNumber - 1) : 1;
            var nextPage = pageNumber < totalPage ? (pageNumber + 1) : pageNumber;
            var bookResult = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new BookList
            {
                TotalPage = totalPage,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                PreviousPage = previousPage,
                NextPage = nextPage,
                Books = _bookMapper.Map<List<Book>, List<BookDto>>(bookResult)
            }; ;
        }

        public async Task<BookDto> GetBookById(Guid id)
        {
            var bookDetail = await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (bookDetail == null)
            {
                return null;
            }

            var bookResult = _bookMapper.Map<Book, BookDto>(bookDetail);
            return bookResult;
        }

        public async Task<bool> UpdateBook(Guid id, BookDto bookDto)
        {
            var existedBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (existedBook == null)
            {
                return false;
            }

            existedBook = _bookMapper.Map(bookDto, existedBook);
            await _bookRepository.UpdateAsync(existedBook);
            return true;
        }
        public async Task<bool> CreateBook(BookDto bookDto)
        {
            Book book;
            book = new Book();
            if (bookDto == null)
            {
                return false;
            }

            var bookToUpdate = _bookMapper.Map<BookDto, Book>(bookDto);
            await _bookRepository.InsertAsync(bookToUpdate);
            return true;
        }
        public async Task<bool> DeleteBook(Guid id)
        {
            var bookToDetele = await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (bookToDetele == null)
            {
                return false;
            }

            await _bookRepository.DeleteAsync(bookToDetele);
            return true;
        }
    }

}

