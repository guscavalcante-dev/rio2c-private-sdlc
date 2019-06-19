using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProjectSummaryPtBrIsRequired : ISpecification<Project>
    {
        public string Target { get { return "Summaries"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Project entity)
        {           
            return entity.Summaries != null && entity.Summaries.Any( e => e.LanguageCode == LanguageCodes.PtBr.ToString() && !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
