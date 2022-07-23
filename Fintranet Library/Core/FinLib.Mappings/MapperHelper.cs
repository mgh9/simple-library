using AutoMapper;

namespace FinLib.Mappings
{
    public static class MapperHelper
    {
        static MapperHelper()
        {
            Mapper = MappingProfiles.Init();
        }

        public static IMapper Mapper { get; }

        public static T MapTo<T>(object entity)
        {
            return Mapper.Map<T>(entity);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
