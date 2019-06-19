using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class ImageExtensions
    {
        private const int exifOrientationID = 274; //274   0x112

        public static byte[] GetImageFromStringPath(this string path, System.Drawing.Imaging.ImageFormat format)
        {
            using (Image img = Image.FromFile(@path))
            {
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, format);
                    arr = ms.ToArray();
                }
                return arr;
            }
        }

        public static byte[] ImageToByte(this Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static byte[] StreamToByteArray(this Stream input)
        {
            using (MemoryStream target = new MemoryStream())
            {
                input.CopyTo(target);
                byte[] data = target.ToArray();
                return data;
            }
            //byte[] buffer = new byte[16 * 1024];
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    int read;
            //    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, 0, read);
            //    }
            //    return ms.ToArray();
            //}
        }

        public static byte[] HttpPostedFileBaseToByteArray(this HttpPostedFileBase postFile)
        {
            byte[] file = new byte[postFile.ContentLength];
            postFile.InputStream.Read(file, 0, file.Length);
            return file;
        }

        public static void ExifRotate(Image imgRef, Bitmap img)
        {
            if (!imgRef.PropertyIdList.Contains(exifOrientationID))
                return;

            var prop = imgRef.GetPropertyItem(exifOrientationID);
            int val = BitConverter.ToUInt16(prop.Value, 0);
            var rot = RotateFlipType.RotateNoneFlipNone;

            if (val == 3 || val == 4)
                rot = RotateFlipType.Rotate180FlipNone;
            else if (val == 5 || val == 6)
                rot = RotateFlipType.Rotate90FlipNone;
            else if (val == 7 || val == 8)
                rot = RotateFlipType.Rotate270FlipNone;

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot != RotateFlipType.RotateNoneFlipNone)
                img.RotateFlip(rot);
        }

        public static byte[] ResizeToMaxBytes(byte[] byteImageIn, int maxbyte)
        {
            try
            {
                byte[] currentByteImageArray = byteImageIn;
                double scale = 0.5f;

                MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
                System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(inputMemoryStream);

                while (currentByteImageArray.Length > maxbyte)
                {
                    Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));

                    MemoryStream resultStream = new MemoryStream();

                    ExifRotate(fullsizeImage, fullSizeBitmap);

                    fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                    currentByteImageArray = resultStream.ToArray();
                    resultStream.Dispose();
                    resultStream.Close();

                    scale -= 0.05f;
                }

                return currentByteImageArray;
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }

        public static byte[] Compreess(byte[] byteImageIn, System.Int64 quality = 100)
        {
            try
            {
                byte[] currentByteImageArray = byteImageIn;

                MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
                System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(inputMemoryStream);

                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width), (int)(fullsizeImage.Height)));

                MemoryStream resultStream = new MemoryStream();

                EncoderParameters imgParams = new EncoderParameters(1);
                imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, quality) };
                ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);

                ExifRotate(fullsizeImage, fullSizeBitmap);

                fullSizeBitmap.Save(resultStream, codec, imgParams);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                return currentByteImageArray;
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }


        public static bool IsSatisfiedByMinAndMaxSize(byte[] byteImageIn, double imageMinMByteSize, double imageMaxMByteSize)
        {
            if (byteImageIn != null)
            {
                try
                {
                    using (var img = Image.FromStream(new MemoryStream(byteImageIn)))
                    {
                        var resultMbyte = ConvertBytesToMegabytes(byteImageIn.Count());

                        return (resultMbyte >= imageMinMByteSize && resultMbyte <= imageMaxMByteSize);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public static bool IsSatisfiedByFormat(byte[] byteImageIn, ImageFormat[] validFormats)
        {
            if (byteImageIn != null)
            {
                try
                {
                    //var validFormats = new ImageFormat[3] { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png };
                    using (var img = Image.FromStream(new MemoryStream(byteImageIn)))
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

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
