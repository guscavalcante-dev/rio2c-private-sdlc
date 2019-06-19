using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ImageBase64AppViewModel : EntityViewModel<ImageBase64AppViewModel, Domain.Entities.ImageFile>, IEntityViewModel<Domain.Entities.ImageFile>
    {
        public string FileName { get; set; }
        public string File { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }

        public ImageBase64AppViewModel()
        {

        }

        public ImageBase64AppViewModel(HttpPostedFileBase imageUpload)
        {
            FileName = imageUpload.FileName;
            File = Convert.ToBase64String(imageUpload.GetBytes());
            ContentType = imageUpload.ContentType;
            ContentLength = imageUpload.ContentLength;
        }

        public ImageBase64AppViewModel(ImageFile image)
        {
            FileName = image.FileName;
            File = Convert.ToBase64String(image.File);
            ContentType = image.ContentType;
            ContentLength = image.ContentLength;
        }

        public ImageFile MapReverse()
        {
            var imageFile = new ImageFile(FileName, Convert.FromBase64String(File), ContentType, ContentLength);

            return imageFile;            
        }

        public ImageFile MapReverse(ImageFile entity)
        {
            entity.SetFile(Convert.FromBase64String(this.File));
            entity.SetFileName(this.FileName);
            entity.SetContentLength(this.ContentLength);
            entity.SetContentType(this.ContentType);
            return entity;
        }

        public static ImageFileAppViewModel GetThumbImage(ImageFile image)
        {
            var newImageFile = Resize2Max50Kbytes(image.File);

            var newImage = new ImageFile(image.FileName, newImageFile, image.ContentType, image.ContentLength);

            return new ImageFileAppViewModel(newImage);
        }

        private static byte[] Resize2Max50Kbytes(byte[] byteImageIn)
        {
            byte[] currentByteImageArray = byteImageIn;
            double scale = 0.2f;

            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 50000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }
    }
}
