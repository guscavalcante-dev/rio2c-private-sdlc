using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProjectMustHavePitching : ISpecification<Project>
    {
        public string Target { get { return "Pitching"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Project entity)
        {
            return entity.Pitching != null;
        }
    }
}
