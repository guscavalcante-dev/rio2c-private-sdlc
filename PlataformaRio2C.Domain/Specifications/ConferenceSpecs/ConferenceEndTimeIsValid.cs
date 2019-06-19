using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ConferenceEndTimeIsValid : ISpecification<Conference>
    {
        public string Target { get { return "EndTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Conference entity)
        {
            TimeSpan ts;
            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.EndTime.ToString(), CultureInfo.InvariantCulture, out ts);
         
            return entity.EndTime != null && TruckisValidTimeSpan && ts.Days == 0;
        }
    }
}
