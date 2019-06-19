using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProjectSummaryEnIsRequired : ISpecification<Project>
    {
        public string Target { get { return "Summaries"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Project entity)
        {           
            return entity.Summaries != null && entity.Summaries.Any( e => e.LanguageCode == LanguageCodes.En.ToString() && !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
