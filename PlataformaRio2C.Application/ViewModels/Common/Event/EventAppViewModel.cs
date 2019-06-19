using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.ViewModels
{
    public class EventAppViewModel : IEntityViewModel<Domain.Entities.Event>
    {
        public string Name { get; set; }

        public EventAppViewModel()
        {

        }

        public EventAppViewModel(Domain.Entities.Event entity)
        {
            Name = entity.Name;
        }

        public Event MapReverse()
        {
            return new Domain.Entities.Event(this.Name);
        }

        public Event MapReverse(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
