//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System;
//using System.Globalization;
//using System.Text.RegularExpressions;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class NegotiationConfigTimeIntervalBetweenTurnIsValid : ISpecification<NegotiationConfig>
//    {
//        public string Target { get { return "TimeIntervalBetweenTurn"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(NegotiationConfig entity)
//        {
//            TimeSpan ts;
//            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.TimeIntervalBetweenTurn.ToString(), CultureInfo.InvariantCulture, out ts);

//            return entity.TimeIntervalBetweenTurn != null && TruckisValidTimeSpan && ts.Days == 0;
//        }
//    }
//}
