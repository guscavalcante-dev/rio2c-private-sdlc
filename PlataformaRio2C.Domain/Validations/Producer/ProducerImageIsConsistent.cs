using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class ProducerImageIsConsistent : Validation<Producer>
    {
        public ProducerImageIsConsistent()
        {
            base.AddRule(new ValidationRule<Producer>(new ProducerResolutionFoto(), Messages.InvalidResolutionPlayer));
            base.AddRule(new ValidationRule<Producer>(new ProducerLogoMustHaveValidFormat(),Messages.InvalidExtensionPng));
        }
    }
}
