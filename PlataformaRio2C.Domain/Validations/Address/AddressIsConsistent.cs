using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class AddressIsConsistent : Validation<Address>
    {
        public AddressIsConsistent()
        {
            base.AddRule(new ValidationRule<Address>(new AddressZipCodeRequired(), Messages.ZipCodeIsRequired));
            //base.AddRule(new ValidationRule<Address>(new AddressCountryIsRequired(), Messages.CountryIsRequired));
            //base.AddRule(new ValidationRule<Address>(new AddressCountryIsValid(), Messages.CountryIsInvalid));

            base.AddRule(new ValidationRule<Address>(new AddressStateIsRequired(), Messages.StateIsRequired));
            base.AddRule(new ValidationRule<Address>(new AddressStateIsValid(), Messages.StateIsInvalid));            

            base.AddRule(new ValidationRule<Address>(new AddressCityIsRequired(), Messages.CityIsRequired));
            base.AddRule(new ValidationRule<Address>(new AddressCityIsValid(), Messages.CityIsInvalid));

            base.AddRule(new ValidationRule<Address>(new AddressValueIsRequired(), Messages.AddressIsRequired));            
        }
    }
}
