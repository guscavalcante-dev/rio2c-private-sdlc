using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsArrivalTimeIsRequired : ISpecification<Logistics>
    {
        public string Target { get { return "ArrivalTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Logistics entity)
        {
            return entity.ArrivalTime != null;
        }
    }
}
