using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos.Agendas
{
    public class CollaboratorEventsDto
    {
        public Guid CollaboratorUid { get; set; }
        public List<CollaboratorEventDto> CollaboratorEventDtos { get; set; }
    }
}
