using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsArrivalDateIsRequired : ISpecification<Logistics>
    {
        public string Target { get { return "ArrivalDate"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Logistics entity)
        {
            return entity.ArrivalDate != null;
        }
    }
}
