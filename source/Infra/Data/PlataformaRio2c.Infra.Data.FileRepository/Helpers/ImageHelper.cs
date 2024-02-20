// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-22-2024
// ***********************************************************************
// <copyright file="ImageHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2c.Infra.Data.FileRepository.Helpers
{
    /// <summary>
    /// ImageHelper
    /// </summary>
    public static class ImageHelper
    {
        private static readonly List<string> AllowedImageFormats = new List<string> { "jpg", "jpeg", "png", "gif", "image/jpg", "image/jpeg", "image/png", "image/gif" };

        /// <summary>Uploads the original and cropped images.</summary>
        /// <param name="fileUid">The file uid.</param>
        /// <param name="file">The file.</param>
        /// <param name="dataX">The data x.</param>
        /// <param name="dataY">The data y.</param>
        /// <param name="dataWidth">Width of the data.</param>
        /// <param name="dataHeight">Height of the data.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        public static void UploadOriginalAndCroppedImages(Guid fileUid, HttpPostedFileBase file, decimal dataX, decimal dataY, decimal dataWidth, decimal dataHeight, FileRepositoryPathType fileRepositoryPathType)
        {
            if (AllowedImageFormats.All(aif => aif != file.ContentType))
            {
                throw new DomainException("[[[The image format must be jpeg, jpg, gif or png.]]]");
            }

            byte[] imageBytes = null;

            // The file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageBytes = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            else
            {
                throw new DomainException("The file was not uploaded.");
            }

            // Original image
            UploadLogo(fileUid, imageBytes, fileRepositoryPathType, true);

            // Thumbnail image 200x200
            var croppedImage200 = CropImage(imageBytes, dataX, dataY, dataWidth, dataHeight, true, 200, 200, fileUid);
            UploadLogo(fileUid, croppedImage200.GetBytes(), fileRepositoryPathType, false);

            // Thumbnail imagem 500x500
            var croppedImage500 = CropImage(imageBytes, dataX, dataY, dataWidth, dataHeight, true, 500, 500, fileUid);
            UploadLogo(fileUid, croppedImage500.GetBytes(), fileRepositoryPathType, false, "_500x500");
        }

        /// <summary>
        /// Uploads the original and cropped images.
        /// </summary>
        /// <param name="fileUid">The file uid.</param>
        /// <param name="base64Image">The base64 image.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <exception cref="DomainException">
        /// [[[The image format must be jpeg, jpg, gif or png.]]]
        /// or
        /// The file was not uploaded.
        /// </exception>
        public static void UploadOriginalAndThumbnailImages(Guid fileUid, string base64Image, FileRepositoryPathType fileRepositoryPathType)
        {
            if (AllowedImageFormats.All(aif => aif != base64Image.GetBase64FileExtension().Replace(".", "")))
            {
                throw new DomainException("[[[The image format must be jpeg, jpg, gif or png.]]]");
            }

            byte[] imageBytes = null;

            // The file was uploaded
            if (!string.IsNullOrEmpty(base64Image) && base64Image.IsBase64String())
            {
                imageBytes = Convert.FromBase64String(base64Image);
            }
            else
            {
                throw new DomainException("The file was not uploaded.");
            }

            // Original image
            UploadLogo(fileUid, imageBytes, fileRepositoryPathType, true);

            // Thumbnail image 200x200
            var croppedImage200 = BasicCrop(imageBytes, 200, 200);
            UploadLogo(fileUid, croppedImage200.GetBytes(), fileRepositoryPathType, false);

            // Thumbnail imagem 500x500
            var croppedImage500 = BasicCrop(imageBytes, 500, 500);
            UploadLogo(fileUid, croppedImage500.GetBytes(), fileRepositoryPathType, false, "_500x500");
        }

        /// <summary>Deletes the original and cropped images.</summary>
        /// <param name="fileUid">The file uid.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        public static void DeleteOriginalAndCroppedImages(Guid fileUid, FileRepositoryPathType fileRepositoryPathType)
        {
            var fileRepo = new FileRepositoryFactory().Get();
            fileRepo.DeleteImages(fileUid, fileRepositoryPathType);
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="imageUid">The image uid.</param>
        /// <param name="imageUploadDate">The image upload date.</param>
        /// <param name="isThumbnail">if set to <c>true</c> [is thumbnail].</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        /// <returns></returns>
        public static string GetImageUrl(FileRepositoryPathType fileRepositoryPathType, Guid imageUid, DateTimeOffset? imageUploadDate, bool isThumbnail, string additionalFileInfo = "")
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return fileRepo.GetImageUrl(fileRepositoryPathType, imageUid, imageUploadDate, isThumbnail, additionalFileInfo);
        }

        /// <summary>
        /// Gets the video URL.
        /// </summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="objectUid">The object uid.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="videoExtension">The video extension.</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        /// <returns></returns>
        public static string GetVideoUrl(FileRepositoryPathType fileRepositoryPathType, Guid objectUid, DateTimeOffset? uploadDate, string videoExtension = ".mp4", string additionalFileInfo = "")
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return fileRepo.GetFileUrl(fileRepositoryPathType, objectUid, uploadDate, fileExtension: videoExtension, additionalFileInfo: additionalFileInfo);
        }

        /// <summary>
        /// Gets the no image URL.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileRepositoryFactory"></exception>
        public static string GetNoImageUrl()
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return $"{fileRepo.GetBaseUrl()}/no-image.png";
        }

        #region Private Methods

        /// <summary>Uploads the logo.</summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="imageBytes">The image bytes.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="isOriginalLogo">if set to <c>true</c> [is original logo].</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        private static void UploadLogo(Guid siteId, byte[] imageBytes, FileRepositoryPathType fileRepositoryPathType, bool isOriginalLogo, string additionalFileInfo = null)
        {
            IFileRepository fileRepo = new FileRepositoryFactory().Get();

            using (var originalImageStream = new MemoryStream(imageBytes))
            {
                fileRepo.Upload(originalImageStream, "image/png", siteId + (isOriginalLogo ? $"_original{additionalFileInfo}.png" : $"_thumbnail{additionalFileInfo}.png"), fileRepositoryPathType);
            }
        }

        /// <summary>Basics the crop.</summary>
        /// <param name="imageBytes">The image bytes.</param>
        /// <param name="cropImageWidth">Width of the crop image.</param>
        /// <param name="cropImageHeight">Height of the crop image.</param>
        /// <param name="preventEnlarge">if set to <c>true</c> [prevent enlarge].</param>
        /// <param name="cropFirstLineAndColumn">if set to <c>true</c> [crop first line and column].</param>
        /// <returns></returns>
        private static WebImage BasicCrop(byte[] imageBytes, int cropImageWidth, int cropImageHeight, bool preventEnlarge = false, bool cropFirstLineAndColumn = false)
        {
            var image = new WebImage(imageBytes)
                                .Resize(cropImageWidth, cropImageHeight, true, preventEnlarge);

            if (cropFirstLineAndColumn)
            {
                return image.Crop(1, 1);

            }

            return image;
        }

        /// <summary>Crops the image.</summary>
        /// <param name="content">The content.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="basicCropAfter">if set to <c>true</c> [basic crop after].</param>
        /// <param name="basicCropImageWidth">Width of the basic crop image.</param>
        /// <param name="basicCropImageHeight">Height of the basic crop image.</param>
        /// <returns></returns>
        private static WebImage CropImage(byte[] content, decimal? x, decimal? y, decimal? width, decimal? height, bool basicCropAfter, int? basicCropImageWidth, int? basicCropImageHeight, Guid? fileUid = null)
        {
            byte[] croppedImage = null;
            if (width.HasValue && height.HasValue && x.HasValue && y.HasValue)
            {
                var cropWidth = Decimal.ToInt32(width.Value);
                var cropHeight = Decimal.ToInt32(height.Value);
                var cropPointX = Decimal.ToInt32(x.Value);
                var cropPointY = Decimal.ToInt32(y.Value);

                croppedImage = ImageHelper.CropImage(content, cropPointX, cropPointY, cropWidth, cropHeight);
            }
            else
            {
                croppedImage = content;
            }

            if (basicCropAfter && basicCropImageWidth.HasValue && basicCropImageHeight.HasValue)
            {
                return BasicCrop(croppedImage, basicCropImageWidth.Value, basicCropImageHeight.Value);
            }

            return new WebImage(croppedImage);
        }

        /// <summary>Crops the image.</summary>
        /// <param name="content">The content.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private static byte[] CropImage(byte[] content, int x, int y, int width, int height)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                return CropImage(stream, x, y, width, height);
            }
        }

        /// <summary>Crops the image.</summary>
        /// <param name="content">The content.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private static byte[] CropImage(Stream content, int x, int y, int width, int height)
        {
            //Parsing stream to bitmap
            Bitmap sourceBitmap = null;

            try
            {
                sourceBitmap = new Bitmap(content);
            }
            catch
            {
                throw new DomainException(Messages.InvalidImageFormat);
            }

            using (sourceBitmap)
            {
                var sourceBitmapFixed = FixOrientation(sourceBitmap);

                if (width <= 0)
                {
                    width = 200;
                }

                if (height <= 0)
                {
                    height = 200;
                }

                //Get new dimensions
                Rectangle cropRect = new Rectangle(x, y, width, height);

                try
                {
                    //Creating new bitmap with valid dimensions
                    using (Bitmap newBitMap = new Bitmap(cropRect.Width, cropRect.Height))
                    {
                        using (Graphics g = Graphics.FromImage(newBitMap))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            g.DrawImage(sourceBitmapFixed, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);

                            return GetBitmapBytes(newBitMap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new DomainException($"Error at new Bitmap() => {cropRect.Width} {cropRect.Height} {cropRect.ToString()} {ex.GetInnerMessage()} ");
                }
            }
        }

        /// <summary>Gets the bitmap bytes.</summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static byte[] GetBitmapBytes(Bitmap source)
        {
            //Settings to increase quality of the image
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            //Temporary stream to save the bitmap
            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }

        /// <summary>Fixes the orientation.</summary>
        /// <param name="img">The img.</param>
        /// <returns></returns>
        private static System.Drawing.Bitmap FixOrientation(System.Drawing.Bitmap img)
        {
            if (Array.IndexOf(img.PropertyIdList, 274) > -1)
            {
                var orientation = (int)img.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;
                    case 2:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        img.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        img.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                // This EXIF data is now invalid and should be removed.
                img.RemovePropertyItem(274);
            }

            return img;
        }

        #endregion
    }
}