using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class ImageIsConsistent : Validation<ImageFile>
    {
        public ImageIsConsistent()
        {
            base.AddRule(new ValidationRule<ImageFile>(new ImageIsRequired(), Messages.ImageIsRequired));
            base.AddRule(new ValidationRule<ImageFile>(new ImageMustHaveValidFormat(), Messages.InvalidImageFormat));
        }
    }
}
