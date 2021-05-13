using Acme.ProjectCompare.Model;
using Acme.ProjectCompare.Samples;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Acme.ProjectCompare
{
    public class ProjectCompareApplicationAutoMapperProfile : Profile
    {
        public ProjectCompareApplicationAutoMapperProfile()
        {
            CreateMap<Book, BookDto>().ForMember(b => b.BookId, opt => opt.MapFrom(p => p.Id));
        }
    }
}