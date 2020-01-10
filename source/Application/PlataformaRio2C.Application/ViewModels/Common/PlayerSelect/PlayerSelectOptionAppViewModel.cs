//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Drawing;
//using System.IO;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerSelectOptionAppViewModel
//    {
//        public Guid Uid { get; set; }

//        [Display(Name = "Name", ResourceType = typeof(Labels))]
//        public string Name { get; set; }       

//        public bool HasImage { get; set; }

//        public ImageFileAppViewModel Image { get; set; }

//        public PlayerSelectOptionAppViewModel()
//        {

//        }

//        public PlayerSelectOptionAppViewModel(Player entity)
//        {
//            Name = entity.Name;
//            Uid = entity.Uid;
//            HasImage = entity.ImageId > 0;
//        }

//        public PlayerSelectOptionAppViewModel(Player entity, bool bindImage)
//        {
//            Name = entity.Name;
//            Uid = entity.Uid;
//            HasImage = entity.ImageId > 0;

//            if (bindImage)
//            {
//                Image = GetImage(entity.Image);
//            }            
//        }

//        public static ImageFileAppViewModel GetThumbImage(ImageFile image)
//        {
//            var newImageFile = Resize2Max50Kbytes(image.File);

//            var newImage = new ImageFile(image.FileName, newImageFile, image.ContentType, image.ContentLength);

//            return new ImageFileAppViewModel(newImage);
//        }

//        public static ImageFileAppViewModel GetImage(ImageFile image)
//        {
//            var newImage = new ImageFile(image.FileName, image.File, image.ContentType, image.ContentLength);

//            return new ImageFileAppViewModel(newImage);
//        }

//        private static byte[] Resize2Max50Kbytes(byte[] byteImageIn)
//        {
//            byte[] currentByteImageArray = byteImageIn;
//            double scale = 0.2f;
            
//            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
//            System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(inputMemoryStream);

//            while (currentByteImageArray.Length > 50000)
//            {
//                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
//                MemoryStream resultStream = new MemoryStream();

//                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

//                currentByteImageArray = resultStream.ToArray();
//                resultStream.Dispose();
//                resultStream.Close();

//                scale -= 0.05f;
//            }

//            return currentByteImageArray;
//        }


//        public static IEnumerable<PlayerSelectOptionAppViewModel> MapList(IEnumerable<Player> entities)
//        {
//            foreach (var entity in entities)
//            {                
//                yield return new PlayerSelectOptionAppViewModel(entity);
//            }
//        }
//    }
//}
