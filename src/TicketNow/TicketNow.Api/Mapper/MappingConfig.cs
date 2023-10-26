using AutoMapper;
using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Dtos.Event;
using TicketNow.Domain.Dtos.User;
using TicketNow.Domain.Dtos.Order;
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

                #region Order

                config.CreateMap<OrderDto, Order>().ReverseMap();
                config.CreateMap<AddOrderDto, OrderDto>().ReverseMap();
                config.CreateMap<OrderItemDto, OrderItem>().ReverseMap();
                config.CreateMap<AddOrderItemDto, OrderItem>().ReverseMap();

                #endregion

                #region Event
                config.CreateMap<EventDto, Event>().ReverseMap();
                config.CreateMap<AddEventDto, Event>().ReverseMap();
                config.CreateMap<UpdateEventDto, Event>().ReverseMap();
                #endregion

                #region User
                config.CreateMap<UserResponseDto, User>().ReverseMap();
                config.CreateMap<UpdateUserDto, User>().ReverseMap();
                #endregion
            });
            return mappingConfig;
        }
    }
}
