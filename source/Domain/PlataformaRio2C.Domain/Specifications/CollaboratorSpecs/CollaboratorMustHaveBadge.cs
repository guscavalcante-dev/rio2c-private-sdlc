using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class CollaboratorMustHaveBadge : ISpecification<Collaborator>
    {
        public string Target { get { return "Badge"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Collaborator entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Badge);
        }
    }
}
