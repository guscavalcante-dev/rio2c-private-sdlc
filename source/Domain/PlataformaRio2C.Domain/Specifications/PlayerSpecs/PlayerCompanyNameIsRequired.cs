using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class PlayerCompanyNameIsRequired : ISpecification<Player>
    {
        public string Target { get { return "CompanyName"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Player entity)
        {
            return !string.IsNullOrWhiteSpace(entity.CompanyName);
        }
    }
}
