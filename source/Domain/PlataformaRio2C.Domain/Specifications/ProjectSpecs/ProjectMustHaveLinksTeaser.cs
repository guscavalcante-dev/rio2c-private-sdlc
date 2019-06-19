using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProjectMustHaveLinksTeaser : ISpecification<Project>
    {
        public string Target { get { return "LinksTeaser"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Project entity)
        {
            return entity.LinksTeaser != null && entity.LinksTeaser.Any(e => !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
