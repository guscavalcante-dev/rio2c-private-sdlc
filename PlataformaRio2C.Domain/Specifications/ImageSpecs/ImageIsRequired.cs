using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ImageIsRequired : ISpecification<ImageFile>
    {
        public string Target { get { return "ImageUpload"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(ImageFile entity)
        {
            if (entity == null) return false;

            return entity != null && entity.File != null && entity.File.Length > 0;
        }
    }
}
