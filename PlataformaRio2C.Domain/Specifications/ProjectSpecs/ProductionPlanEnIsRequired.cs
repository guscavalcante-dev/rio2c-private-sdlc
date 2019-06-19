using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProductionPlanEnIsRequired : ISpecification<Project>
    {
        public string Target { get { return "ProductionPlans"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Project entity)
        {           
            return entity.ProductionPlans != null && entity.ProductionPlans.Any( e => e.LanguageCode == LanguageCodes.En.ToString() && !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
