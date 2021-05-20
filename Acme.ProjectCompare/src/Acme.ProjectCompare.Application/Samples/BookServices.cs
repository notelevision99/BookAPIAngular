using Acme.ProjectCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Acme.ProjectCompare.Samples
{
    public class BookServices : ApplicationService, IBookServices
    {
        private readonly IRepository<Book, Guid> _bookRepository;
       
        private readonly IObjectMapper<ProjectCompareApplicationModule> _bookMapper;

        public BookServices(IRepository<Book, Guid> bookRepository, IObjectMapper<ProjectCompareApplicationModule> bookMapper)
        {
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
        }
        public async Task<BookList> GetBooks(int pageSize, int pageNumber, string searchString)
        {
            int totalCount;
            int totalPage;
            int previousPage;
            int nextPage;
            IQueryable<BookDto> source;
            BookList result;
            List<BookDto> bookResult;
            if (searchString == null)
            {
                source = _bookRepository.Select(p => _bookMapper.Map<Book, BookDto>(p)).AsQueryable();
            }
            else
            {
                source = _bookRepository.Where(p => p.BookName.Contains(searchString)).Select(p => _bookMapper.Map<Book, BookDto>(p)).AsQueryable();
            }
            totalCount = source.Count();
            totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            previousPage = pageNumber > 1 ? (pageNumber - 1) : 1;
            nextPage = pageNumber < totalPage ? (pageNumber + 1) : pageNumber;
            bookResult = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new BookList
            {
                TotalPage = totalPage,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                PreviousPage = previousPage,
                NextPage = nextPage,
                Books = bookResult
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
