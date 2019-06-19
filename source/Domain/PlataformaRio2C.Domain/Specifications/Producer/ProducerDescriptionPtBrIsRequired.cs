using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProducerDescriptionPtBrIsRequired : ISpecification<Producer>
    {
        public string Target { get { return "Descriptions"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Producer entity)
        {           
            return entity.Descriptions != null && entity.Descriptions.Any( e => e.LanguageCode == LanguageCodes.PtBr.ToString() && !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
