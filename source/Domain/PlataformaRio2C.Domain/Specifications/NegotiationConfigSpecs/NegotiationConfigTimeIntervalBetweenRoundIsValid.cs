using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationConfigTimeIntervalBetweenRoundIsValid : ISpecification<NegotiationConfig>
    {
        public string Target { get { return "TimeIntervalBetweenRound"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationConfig entity)
        {
            TimeSpan ts;
            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.TimeIntervalBetweenRound.ToString(), CultureInfo.InvariantCulture, out ts);

            return entity.TimeIntervalBetweenRound != null && TruckisValidTimeSpan && ts.Days == 0;
        }
    }
}
