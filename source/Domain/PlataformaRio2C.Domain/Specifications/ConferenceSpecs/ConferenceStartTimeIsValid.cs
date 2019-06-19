using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ConferenceStartTimeIsValid : ISpecification<Conference>
    {
        public string Target { get { return "StartTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Conference entity)
        {
            TimeSpan ts;
            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.StartTime.ToString(), CultureInfo.InvariantCulture, out ts);

            return entity.StartTime != null && TruckisValidTimeSpan && ts.Days == 0;
        }
    }
}
