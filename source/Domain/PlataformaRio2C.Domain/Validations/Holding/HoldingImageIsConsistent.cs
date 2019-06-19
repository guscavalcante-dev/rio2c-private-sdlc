using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class HoldingImageIsConsistent : Validation<Holding>
    {
        public HoldingImageIsConsistent()
        {
            base.AddRule(new ValidationRule<Holding>(new HoldingResolutionFoto(), Messages.InvalidResolutionPlayer));
            base.AddRule(new ValidationRule<Holding>(new HoldingLogoMustHaveValidFormat(), Messages.InvalidExtensionPng));
        }
    }
}
