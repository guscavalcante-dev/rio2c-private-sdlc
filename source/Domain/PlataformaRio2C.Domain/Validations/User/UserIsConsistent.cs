using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class UserIsConsistent : Validation<User>
    {
        public UserIsConsistent()
        {
            base.AddRule(new ValidationRule<User>(new UserEmailIsValid(), Messages.EmailISInvalid));
        }      
    }
}
