using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.Event;
using TicketNow.Domain.Filters;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IEventService
    {
        Task<DefaultServiceResponseDto> AddEventAsync(AddEventDto addEventDto);
        DefaultServiceResponseDto DeleteEvent(int eventId);
        ICollection<EventDto> GetAllEvents(EventFilter filter);
        ICollection<EventDto> GetAllEventsByPromoter(EventFilter filter, int promoterId);
        EventDto GetEvent(int eventId);
        Task<DefaultServiceResponseDto> SetState(int eventId, bool active);
        Task<DefaultServiceResponseDto> UpdateEventAsync(UpdateEventDto updateEventDto);
    }
}
