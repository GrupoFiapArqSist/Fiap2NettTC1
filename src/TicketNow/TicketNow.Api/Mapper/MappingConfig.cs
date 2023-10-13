using AutoMapper;
using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Dtos.Event;
using TicketNow.Domain.Entities;

namespace TicketNow.Api.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                #region Auth
                config.CreateMap<RegisterDto, User>().ReverseMap();
                #endregion

                #region Event
                config.CreateMap<EventDto, Event>().ReverseMap();
                config.CreateMap<AddEventDto, Event>().ReverseMap();
                #endregion
            });
            return mappingConfig;
        }
    }
}
