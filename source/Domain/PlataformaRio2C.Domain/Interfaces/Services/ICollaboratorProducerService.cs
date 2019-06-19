using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICollaboratorProducerService : IService<Collaborator>
    {
        ValidationResult UpdateByPortal(Collaborator entity);
        ValidationResult PreRegister(string email, string cnpj, string collaboratorName, Event eventEntity);
    }
}
