//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System;
//using System.Drawing;
//using System.IO;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ImageResolutionFoto : ISpecification<ImageFile>
//    {
//        public string Target { get { return "ImageUpload"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(ImageFile entity)
//        {
//            if (entity != null && entity.File != null)
//            {
//                try
//                {
//                    using (var img = Image.FromStream(new MemoryStream(entity.File)))
//                    {
//                        var resultMbyte = ImageFile.ConvertBytesToMegabytes(entity.ContentLength);

//                        return (resultMbyte >= ImageFile.ImageMinMByteSize && resultMbyte <= ImageFile.ImageMaxMByteSize);
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

