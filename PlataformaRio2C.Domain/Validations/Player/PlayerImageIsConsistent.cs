using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class PlayerImageIsConsistent : Validation<Player>
    {
        public PlayerImageIsConsistent()
        {
            base.AddRule(new ValidationRule<Player>(new PlayerResolutionFoto(), Messages.InvalidResolutionPlayer));
            base.AddRule(new ValidationRule<Player>(new PlayerLogoMustHaveValidFormat(),Messages.InvalidExtensionPng));
        }
    }
}
