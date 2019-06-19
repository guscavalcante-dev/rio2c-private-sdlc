using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class PhoneNumberIsRequired : ISpecification<string>
    {
        public string Target { get; set; }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public PhoneNumberIsRequired()
        {
            Target = "PhoneNumber";
        }

        public PhoneNumberIsRequired(string target)
        {
            Target = target;
        }      

        public bool IsSatisfiedBy(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }        
    }
}
