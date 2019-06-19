using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Services
{
    public class EventService : Service<Event>, IEventService
    {
        public EventService(IEventRepository repository)
            :base(repository)
        {

        }
    }
}
