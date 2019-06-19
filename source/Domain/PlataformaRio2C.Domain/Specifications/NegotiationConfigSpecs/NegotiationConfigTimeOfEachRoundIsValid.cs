using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationConfigTimeOfEachRoundIsValid : ISpecification<NegotiationConfig>
    {
        public string Target { get { return "TimeOfEachRound"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationConfig entity)
        {
            TimeSpan ts;
            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.TimeOfEachRound.ToString(), CultureInfo.InvariantCulture, out ts);

            return entity.TimeOfEachRound != null && TruckisValidTimeSpan && ts.Days == 0;
        }
    }
}
