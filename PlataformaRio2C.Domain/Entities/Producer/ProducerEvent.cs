using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class ProducerEvent : IEntity
    {
        public Guid Uid { get; set; }

        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public virtual ValidationResult ValidationResult { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
