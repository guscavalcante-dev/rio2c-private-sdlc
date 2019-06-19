using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProjectMustHaveValueStillNeeded : ISpecification<Project>
    {
        public string Target { get { return "ValueStillNeeded"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Project entity)
        {
            return !string.IsNullOrWhiteSpace(entity.ValueStillNeeded);
        }
    }
}
