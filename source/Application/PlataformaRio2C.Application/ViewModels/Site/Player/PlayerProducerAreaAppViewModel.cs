using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerProducerAreaAppViewModel : EntityViewModel<PlayerProducerAreaAppViewModel, Player>
    {        

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public bool HasImage { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public PlayerProducerAreaAppViewModel()
        {

        }

        public PlayerProducerAreaAppViewModel(Player entity)
        {
            Name = entity.Name;
            Uid = entity.Uid;
            HasImage = entity.ImageId > 0;

            if (entity.Interests != null && entity.Interests.Any())
            {
                Genres = entity.Interests.Where(e => e.Interest.InterestGroup.Name.Contains("Gênero")).Select(e => e.Interest.Name);
            }
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
