using System;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class CollaboratorSpeaker : Entity
    {
        public Guid Uid { get; set; }

        public int CollaboratorId { get; set; }
        public virtual Collaborator Collaborator { get; set; }

        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public virtual ValidationResult ValidationResult { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
