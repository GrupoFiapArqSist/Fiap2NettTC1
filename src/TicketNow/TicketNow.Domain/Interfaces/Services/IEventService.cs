using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.Event;
using TicketNow.Domain.Filters;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IEventService
    {
        Task<DefaultServiceResponseDto> AddEventAsync(AddEventDto addEventDto, int promoterId);
        Task<DefaultServiceResponseDto> DeleteEvent(int eventId, int promoterId);
        ICollection<EventDto> GetAllEvents(EventFilter filter, bool approved);
        ICollection<EventDto> GetAllEventsByPromoter(EventFilter filter, int promoterId);
        EventDto GetEvent(int eventId);
        Task<DefaultServiceResponseDto> SetState(int eventId, bool active, int promoterId);
        Task<DefaultServiceResponseDto> UpdateEventAsync(UpdateEventDto updateEventDto, int promoterId);
    }
}
