using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsArrivalDateSmallerDepartureDate : ISpecification<Logistics>
    {
        public string Target { get { return "ArrivalDate"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Logistics entity)
        {

            if (entity.ArrivalDate == entity.DepartureDate)
            {
                return entity.ArrivalTime < entity.DepartureTime;
            }

            return entity.ArrivalDate <= entity.DepartureDate;
        }
    }
}
