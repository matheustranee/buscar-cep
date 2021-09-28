using AutoMapper;

namespace BuscarCep.Domain.AutoMapper
{
    public class AutoMapperProfileSetup
    {
        public static MapperConfiguration RegisterMappingProfiles()
        {
            return new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new DtoToViewModel());
            });
        }
    }
}
