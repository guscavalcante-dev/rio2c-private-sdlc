//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ImageMustHaveValidFormat : ISpecification<ImageFile>
//    {
//        public string Target { get { return "ImageUpload"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(ImageFile entity)
//        {
//            if (entity != null && entity.File != null)
//            {
//                try
//                {
//                    var validFormats = new ImageFormat[3] { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png };
//                    using (var img = Image.FromStream(new MemoryStream(entity.File)))
//                    {
//                        return validFormats.Contains(img.RawFormat);
//                    }
//                }
//                catch (Exception)
//                {
//                    return false;
//                }
//            }

//            return true;           
//        }
//    }
//}
