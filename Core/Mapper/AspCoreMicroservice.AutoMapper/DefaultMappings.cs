using AutoMapper;
using AspCoreMicroservice.Core.Collections;

namespace AspCoreMicroservice.Core.AutoMapper
{
    public class DefaultMappings : Profile
    {
        public DefaultMappings()
        {
            CreateMap(typeof(PagedList<>), typeof(IPagedList<>))
              .ConvertUsing(typeof(PagedListConverter<,>));
        }
    }
}
