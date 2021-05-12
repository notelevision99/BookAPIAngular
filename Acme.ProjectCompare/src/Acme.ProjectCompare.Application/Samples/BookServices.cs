using Acme.ProjectCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace Acme.ProjectCompare.Samples
{
    public class BookServices : ApplicationService, IBookServices
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookServices(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<BookForList> GetBooks(int pageSize, int pageNumber, string searchString)
        {
            int count;
            int currentPage;
            int totalCount;
            int totalPage;
            int previousPage;
            int nextPage;
            if(searchString == null)
            {
                var source = _bookRepository.Select(p => new BookDto
                {
                    BookId = p.Id,
                    BookName = p.BookName,
                    BookType = p.BookType,
                    Description = p.Description
                }).AsQueryable();

                count = source.Count();

                currentPage = pageNumber;

                totalCount = count;

                totalPage = (int)Math.Ceiling(count / (double)pageSize);

                previousPage = currentPage > 1 ? (currentPage - 1) : 1;

                nextPage = currentPage < totalPage ? (currentPage + 1) : currentPage;

                var books = source.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(p => new BookDto
                {
                    BookId = p.BookId,
                    BookName = p.BookName,
                    BookType = p.BookType,
                    Description = p.Description
                }).ToList();
       
                var result = new BookForList { 
                    TotalPage = totalPage, 
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    PreviousPage = previousPage,
                    NextPage = nextPage,
                    Books = books
                };
                return result;
            }
            else
            {
                // source query with search string
                var source = _bookRepository.Where(p => p.BookName.Contains(searchString)).Select(p => new BookDto
                {
                    BookId = p.Id,
                    BookName = p.BookName,
                    BookType = p.BookType,
                    Description = p.Description
                }).AsQueryable();

                count = source.Count();

                currentPage = pageNumber;             

                totalCount = count;

                totalPage = (int)Math.Ceiling(count / (double)pageSize);

                previousPage = currentPage > 1 ? (currentPage - 1) : 1;

                nextPage = currentPage < totalPage ? (currentPage + 1) : totalPage;

                var books = source.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(p => new BookDto
                {
                    BookId = p.BookId,
                    BookName = p.BookName,
                    BookType = p.BookType,
                    Description = p.Description
                }).ToList();
                var result = new BookForList
                {
                    TotalPage = totalPage,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    PreviousPage = previousPage,
                    NextPage = nextPage,
                    Books = books
                };
                return result;
            }
            //count record
            


            //var books = _bookRepository.Select(p => new BookDto
            //{
            //    BookName = p.BookName,
            //    BookType = p.BookType,
            //    Description = p.Description
            //}).ToList();
            //var books = new List<BookDto>() { new BookDto() { BookName = "1234" } };
            
        }
        

        public async Task<BookDto> GetBookById(Guid id)
        {
            var bookDetail = await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (bookDetail == null)
            {
                return null;
            }
            return new BookDto
            {
                BookId = bookDetail.Id,
                BookName = bookDetail.BookName,
                BookType = bookDetail.BookType,
                Description = bookDetail.Description
            };
        }

        public async Task<int> DeleteBook(Guid id)
        {
            var bookForDelete = await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (bookForDelete == null)
            {
                return 0;
            }
            await _bookRepository.DeleteAsync(bookForDelete);
            return 1;
        }


        public async Task<int> UpdateBook(Guid id, BookDto bookDto)
        {
            var existedBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == id);
       
            if (existedBook == null)
            {
                return 0;
            }
            else
            {
                existedBook.BookName = bookDto.BookName;
                existedBook.BookType = bookDto.BookType;
                existedBook.Description = bookDto.Description;
                await _bookRepository.UpdateAsync(existedBook);
                return 1;
            }
        }
        public async Task<int> CreateBook(BookDto bookDto)
        {
            Book book;
            book = new Book();
            if(bookDto != null)
            {
                book.BookName = bookDto.BookName;
                book.BookType = bookDto.BookType;
                book.Description = bookDto.Description;
                await _bookRepository.InsertAsync(book);
                return 1;
            }
            return 0;
        }
        public async Task<int> DeteleBook(Guid id)
        {
            var bookToDetele =  await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
            if(bookToDetele == null)
            {
                return 0;
            }
            else
            {
                await _bookRepository.DeleteAsync(bookToDetele);
                return 1;
            }
                
           
        }
    }
}
