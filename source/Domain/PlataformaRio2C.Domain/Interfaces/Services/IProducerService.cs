using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IProducerService : IService<Producer>
    {
        ValidationResult UpdateByPortal(Producer entity);

        Producer GetByUserIdAndEventId(int userId, int eventId);

        Producer GetImage(Guid uid);
    }
}
