using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProducerLogoMustHaveValidFormat : ISpecification<Producer>
    {
        public string Target { get { return "ImageUpload"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Producer entity)
        {
            if (entity.Image != null && entity.Image.File != null)
            {
                try
                {
                    var validFormats = new ImageFormat[2] { ImageFormat.Png, ImageFormat.Jpeg };
                    using (var img = Image.FromStream(new MemoryStream(entity.Image.File)))
                    {
                        return validFormats.Contains(img.RawFormat) && (entity.Image.ContentType.Contains("png") || entity.Image.ContentType.Contains("jpeg") || entity.Image.ContentType.Contains("jpg"));
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;           
        }
    }
}
