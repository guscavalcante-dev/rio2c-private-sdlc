using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsCollaboratorIsRequired : ISpecification<Logistics>
    {
        public string Target { get { return "AttendeeCollaboratorId"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Logistics entity)
        {
            return entity.AttendeeCollaboratorId > 0;
        }
    }
}
