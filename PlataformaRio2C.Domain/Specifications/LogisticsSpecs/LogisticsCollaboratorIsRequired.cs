using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsCollaboratorIsRequired : ISpecification<Logistics>
    {
        public string Target { get { return "CollaboratorUid"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Logistics entity)
        {
            return entity.CollaboratorId > 0;
        }
    }
}
