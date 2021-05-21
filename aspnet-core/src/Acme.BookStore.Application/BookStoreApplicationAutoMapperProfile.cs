    using Acme.ProjectCompare.Samples;
    using AutoMapper;
    using Volo.Abp.AutoMapper;

    namespace Acme.BookStore
    {
        public class BookStoreApplicationAutoMapperProfile : Profile
        {
            public BookStoreApplicationAutoMapperProfile()
            {
                /* You can configure your AutoMapper mapping configuration here.
                 * Alternatively, you can split your mapping configurations
                 * into multiple profile classes for a better organization. */

                CreateMap<Book, BookDto>().ForMember(b => b.BookId, opt => opt.MapFrom(p => p.Id));
                CreateMap<BookDto, Book>(MemberList.Source).ForMember(b => b.Id, opt => opt.MapFrom(p => p.BookId)).IgnoreAuditedObjectProperties();
            }
        }
    }
