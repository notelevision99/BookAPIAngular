using Volo.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Acme.BookStore;
using Acme.BookStore.Samples;

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
        public async Task<BookList> GetBooks(FilterDto filterDto)
        {
            filterDto.searchString = string.IsNullOrEmpty(filterDto.searchString) ? "" : filterDto.searchString;
            var source =  _bookRepository.Where(b =>
                  b.BookName.Contains(filterDto.searchString) || b.BookType.Contains(filterDto.searchString) || b.Description.Contains(filterDto.searchString)
                ).AsQueryable();

            var totalCount = source.Count();
            var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.pageSize);
            var previousPage = filterDto.pageNumber > 1 ? (filterDto.pageNumber - 1) : 1;
            var nextPage = filterDto.pageNumber < totalPage ? (filterDto.pageNumber + 1) : filterDto.pageNumber;
            var bookResult = await source.Skip((filterDto.pageNumber - 1) * filterDto.pageSize).Take(filterDto.pageSize).ToListAsync();

            return new BookList
            {
                TotalPage = totalPage,
                CurrentPage = filterDto.pageNumber,
                PageSize = filterDto.pageSize,
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
        public async Task<int> CreateBook(BookDto bookDto)
        {
            if (bookDto.BookName == null)
            {
                return 0;
            }
            if(bookDto.BookType == null)
            {
                return -1;
            }
            if(bookDto.Description == null)
            {
                return -2;
            }

            var bookToUpdate = _bookMapper.Map<BookDto, Book>(bookDto);
            await _bookRepository.InsertAsync(bookToUpdate);
            return 1;
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

