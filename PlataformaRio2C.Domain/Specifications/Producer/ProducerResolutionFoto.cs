using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Drawing;
using System.IO;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProducerResolutionFoto : ISpecification<Producer>
    {
        public string Target { get { return "ImageUpload"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Producer entity)
        {
            if (entity.Image != null && entity.Image.File != null)
            {
                try
                {
                    using (var img = Image.FromStream(new MemoryStream(entity.Image.File)))
                    {
                        var resultMbyte = ImageFile.ConvertBytesToMegabytes(entity.Image.ContentLength);

                        return (resultMbyte >= Player.ImageMinMByteSize && resultMbyte <= ImageFile.ImageMaxMByteSize);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

