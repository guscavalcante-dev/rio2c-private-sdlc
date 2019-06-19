using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class PlayerRestrictionsSpecificsPtBrIsRequired : ISpecification<Player>
    {
        public string Target { get { return "RestrictionsSpecifics"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }
        public bool IsSatisfiedBy(Player entity)
        {
            return entity.RestrictionsSpecifics != null && entity.RestrictionsSpecifics.Any(e => e.LanguageCode == LanguageCodes.PtBr.ToString() && !string.IsNullOrWhiteSpace(e.Value));
        }
    }
}
