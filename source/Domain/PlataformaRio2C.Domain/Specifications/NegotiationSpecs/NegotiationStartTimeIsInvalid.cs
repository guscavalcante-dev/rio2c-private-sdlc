//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System;
//using System.Globalization;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class NegotiationStartTimeIsInvalid : ISpecification<Negotiation>
//    {
//        public string Target { get { return "StartTime"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(Negotiation entity)
//        {
//            TimeSpan ts;
//            bool TruckisValidTimeSpan = TimeSpan.TryParse(entity.StarTime.ToString(), CultureInfo.InvariantCulture, out ts);

//            return entity.StarTime != null && TruckisValidTimeSpan && ts.Days == 0;
//        }
//    }
//}
