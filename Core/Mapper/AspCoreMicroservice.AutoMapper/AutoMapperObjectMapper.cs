using AutoMapper;

namespace AspCoreMicroservice.Core.AutoMapper
{
    public class AutoMapperObjectMapper : Mapping.IObjectMapper
    {
        private readonly IMapper mapper;

        public AutoMapperObjectMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }
    }
}
