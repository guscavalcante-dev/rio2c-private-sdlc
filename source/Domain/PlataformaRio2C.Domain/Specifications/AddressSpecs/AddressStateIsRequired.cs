//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Text.RegularExpressions;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class AddressStateIsRequired : ISpecification<Address>
//    {
//        public string Target { get { return "Address.State"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Address entity)
//        {
//            if (entity == null) return false;

//            if (entity.CountryId != 30)
//                return !string.IsNullOrWhiteSpace(entity.State);
//            else
//                return (int)entity.StateId !=0;

//        }
//    }
//}
