using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Globalization;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class LogisticsDepartureTimeIsValid : ISpecification<Logistics>
    {
        public string Target { get { return "DepartureTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Logistics entity)
        {
            TimeSpan ts;
            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.DepartureTime.ToString(), CultureInfo.InvariantCulture, out ts);
         
            return entity.DepartureTime != null && TruckisValidTimeSpan && ts.Days == 0;
        }
    }
}
