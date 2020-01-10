//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProducerMustHaveName : ISpecification<Producer>
//    {
//        public string Target { get { return "Name"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Producer entity)
//        {
//            return !string.IsNullOrWhiteSpace(entity.Name);
//        }
//    }
//}
