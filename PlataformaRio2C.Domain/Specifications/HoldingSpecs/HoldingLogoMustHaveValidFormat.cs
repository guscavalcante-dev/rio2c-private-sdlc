using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class HoldingLogoMustHaveValidFormat : ISpecification<Holding>
    {
        public string Target { get { return "ImageUpload"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Holding entity)
        {
            if (entity.Image != null && entity.Image.File != null)
            {
                try
                {
                    var validFormats = new ImageFormat[3] { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png };
                    using (var img = Image.FromStream(new MemoryStream(entity.Image.File)))
                    {
                        return validFormats.Contains(img.RawFormat);
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
