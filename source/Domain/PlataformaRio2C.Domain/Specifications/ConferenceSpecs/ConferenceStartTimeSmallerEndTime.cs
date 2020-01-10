//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ConferenceStartTimeSmallerEndTime : ISpecification<Conference>
//    {
//        public string Target { get { return "StartTime"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(Conference entity)
//        {
//            return entity.StartTime <= entity.EndTime;
//        }
//    }
//}
