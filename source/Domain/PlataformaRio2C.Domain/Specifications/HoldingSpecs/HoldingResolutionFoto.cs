using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class HoldingResolutionFoto : ISpecification<Holding>
    {
        public string Target { get { return "ImageUpload"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Holding entity)
        {
            // if (entity.Image != null && entity.Image.File != null)
            //{
            //    try
            //    {
            //        using (var img = Image.FromStream(new MemoryStream(entity.Image.File)))
            //        {
            //            var resultMbyte = ImageFile.ConvertBytesToMegabytes(entity.Image.ContentLength);

            //            return (resultMbyte >= Holding.ImageMinMByteSize && resultMbyte <= ImageFile.ImageMaxMByteSize);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }
    }
}

