using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();


        public void SetUid(Guid uid)
        {
            Uid = uid;
        }
    }
}
