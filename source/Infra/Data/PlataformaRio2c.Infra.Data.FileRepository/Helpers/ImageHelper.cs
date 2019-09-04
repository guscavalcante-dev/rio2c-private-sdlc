// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-04-2019
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2c.Infra.Data.FileRepository.Helpers
{
    /// <summary>
    /// ImageHelper
    /// </summary>
    public static class ImageHelper
    {
        private static readonly List<string> AllowedImageFormats = new List<string> { "jpg", "jpeg", "png", "gif", "image/jpg", "image/jpeg", "image/png", "image/gif" };
        private static readonly int imageWidth = 200;
        private static readonly int imageHieght = 200;

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

            var croppedImage = CropImage(imageBytes, dataX, dataY, dataWidth, dataHeight, true);
            UploadLogo(fileUid, imageBytes, fileRepositoryPathType, true);
            UploadLogo(fileUid, croppedImage.GetBytes(), fileRepositoryPathType, false);
        }

        /// <summary>Deletes the original and cropped images.</summary>
        /// <param name="fileUid">The file uid.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        public static void DeleteOriginalAndCroppedImages(Guid fileUid, FileRepositoryPathType fileRepositoryPathType)
        {
            var fileRepo = new FileRepositoryFactory().Get();
            fileRepo.DeleteImages(fileUid, fileRepositoryPathType);
        }

        /// <summary>Gets the image URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="imageUid">The image uid.</param>
        /// <param name="imageUploadDate">The image upload date.</param>
        /// <param name="isThumbnail">if set to <c>true</c> [is thumbnail].</param>
        /// <returns></returns>
        public static string GetImageUrl(FileRepositoryPathType fileRepositoryPathType, Guid imageUid, DateTime? imageUploadDate, bool isThumbnail)
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return fileRepo.GetImageUrl(fileRepositoryPathType, imageUid, imageUploadDate, isThumbnail);
        }

        #region Private Methods

        /// <summary>Uploads the logo.</summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="imageBytes">The image bytes.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="isOriginalLogo">if set to <c>true</c> [is original logo].</param>
        private static void UploadLogo(Guid siteId, byte[] imageBytes, FileRepositoryPathType fileRepositoryPathType, bool isOriginalLogo)
        {
            IFileRepository fileRepo = new FileRepositoryFactory().Get();

            using (var originalImageStream = new MemoryStream(imageBytes))
            {
                fileRepo.Upload(originalImageStream, "image/png", siteId + (isOriginalLogo ? "_original.png" : "_thumbnail.png"), fileRepositoryPathType);
            }
        }

        /// <summary>Basics the crop.</summary>
        /// <param name="imageBytes">The image bytes.</param>
        /// <param name="preventEnlarge">if set to <c>true</c> [prevent enlarge].</param>
        /// <param name="cropFirstLineAndColumn">if set to <c>true</c> [crop first line and column].</param>
        /// <returns></returns>
        private static WebImage BasicCrop(byte[] imageBytes, bool preventEnlarge = false, bool cropFirstLineAndColumn = false)
        {
            var image = new WebImage(imageBytes)
                                .Resize(imageWidth, imageHieght, true, preventEnlarge);

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
        /// <returns></returns>
        private static WebImage CropImage(byte[] content, decimal? x, decimal? y, decimal? width, decimal? height, bool basicCropAfter)
        {
            byte[] croppedImage = null;
            if (width.HasValue && height.HasValue && x.HasValue && y.HasValue)
            {
                // Convert the crop values to int
                try
                {
                    var cropWidth = Decimal.ToInt32(width.Value);
                    var cropHeight = Decimal.ToInt32(height.Value);
                    var cropPointX = Decimal.ToInt32(x.Value);
                    var cropPointY = Decimal.ToInt32(y.Value);

                    croppedImage = ImageHelper.CropImage(content, cropPointX, cropPointY, cropWidth, cropHeight);
                }
                catch (Exception)
                {
                    throw new Exception("Error croppping the image");
                }
            }
            else
            {
                croppedImage = content;
            }

            if (basicCropAfter)
            {
                return BasicCrop(croppedImage);
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
            using (Bitmap sourceBitmap = new Bitmap(content))
            {
                //Get new dimensions
                Rectangle cropRect = new Rectangle(x, y, width, height);

                //Creating new bitmap with valid dimensions
                using (Bitmap newBitMap = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(newBitMap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);

                        return GetBitmapBytes(newBitMap);
                    }
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

        #endregion
    }
}