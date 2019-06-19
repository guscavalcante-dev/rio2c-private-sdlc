using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class PhoneNumberIsConsistent : Validation<string>
    {
        public PhoneNumberIsConsistent()
        {
            base.AddRule(new ValidationRule<string>(new PhoneNumberIsValid(), Messages.NumberIsInvalid));
        }

        public PhoneNumberIsConsistent(string target)
        {
            base.AddRule(new ValidationRule<string>(new PhoneNumberIsRequired(target), Messages.NumberIsRequired));
            base.AddRule(new ValidationRule<string>(new PhoneNumberIsValid(target), Messages.NumberIsInvalid));
        }
    }
}
