using AutoMapper;
using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Entities;

namespace TicketNow.Api.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {                
                config.CreateMap<RegisterDto, User>().ReverseMap();                
            });
            return mappingConfig;
        }
    }
}
