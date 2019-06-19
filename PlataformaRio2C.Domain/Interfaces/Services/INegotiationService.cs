using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface INegotiationService : IService<Negotiation>
    {
        ValidationResult CreateManual(Negotiation entity);
    }
}
