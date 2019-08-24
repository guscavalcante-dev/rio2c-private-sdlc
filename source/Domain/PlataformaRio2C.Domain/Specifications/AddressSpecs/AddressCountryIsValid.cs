//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Text.RegularExpressions;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class AddressCountryIsValid : ISpecification<Address>
//    {
//        public string Target { get { return "Address.Country"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Address entity)
//        {
//            if (entity == null) return false;

//            return !string.IsNullOrWhiteSpace(entity.Country) && WordValid(entity.Country);
//        }

//        bool WordValid(string value)
//        {
//            Regex rgx = new Regex(@"[a-zA-Z]{2,}");
//            return !string.IsNullOrWhiteSpace(value) && rgx.IsMatch(value);
//        }
//    }
//}
