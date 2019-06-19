using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IEntity
    {
        ValidationResult ValidationResult { get; set; }

        Guid Uid { get; set; }

        bool IsValid();
    }
}
