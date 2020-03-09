//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System;
//using System.Globalization;
//using System.Text.RegularExpressions;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class NegotiationConfigEndTimeIsValid : ISpecification<NegotiationConfig>
//    {
//        public string Target { get { return "StartTime"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(NegotiationConfig entity)
//        {
//            TimeSpan ts;
//            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.EndTime.ToString(), CultureInfo.InvariantCulture, out ts);

//            return entity.EndTime != null && TruckisValidTimeSpan && ts.Days == 0;
//        }
//    }
//}
