// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
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
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
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

        ///// <summary>Uploads the original and basic crop logo.</summary>
        ///// <param name="siteId">The site identifier.</param>
        ///// <param name="imageBytes">The image bytes.</param>
        ///// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        //public static void UploadOriginalAndBasicCropLogo(Guid siteId, byte[] imageBytes, FileRepositoryPathType fileRepositoryPathType)
        //{
        //    try
        //    {
        //        // Resize to maximum width and height
        //        var basicCroppedImage = BasicCrop(imageBytes, true, true);

        //        // Centralize the image horizontaly and verticaly
        //        var centralizedCroppedImage = CropImage(
        //            basicCroppedImage.GetBytes(),
        //            (decimal)(basicCroppedImage.Width - logoWidgth) / 2,
        //            (decimal)(basicCroppedImage.Height - logoHeight) / 2,
        //            (decimal)logoWidgth,
        //            (decimal)logoHeight,
        //            false);

        //        UploadLogo(siteId, imageBytes, fileRepositoryPathType, true);
        //        UploadLogo(siteId, centralizedCroppedImage.GetBytes(), fileRepositoryPathType, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        throw new DomainException("The image could not be saved.");
        //    }
        //}

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

        ///// <summary>
        ///// Gets the site logo.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="html">The HTML.</param>
        ///// <param name="siteId">The site identifier.</param>
        ///// <param name="logoUploadDate">The logo upload date.</param>
        ///// <returns></returns>
        //public static string GetSiteLogo<T>(this HtmlHelper<T> html, Guid siteId, DateTime? logoUploadDate)
        //{
        //    IFileRepository fileRepo = new FileAwsRepository();
        //    return fileRepo.GetSiteLogo(siteId, logoUploadDate);
        //}

        ///// <summary>
        ///// Gets the image stream from URL.
        ///// </summary>
        ///// <param name="siteId">The site identifier.</param>
        ///// <param name="url">The URL.</param>
        ///// <returns></returns>
        //public static byte[] GetImageBytesFromUrl(Guid siteId, string url)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(url))
        //        {
        //            //Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ImageHelper.GetImageBytesFromUrl::(1) The image url is required."));
        //            throw new DomainException("The image url is required.");
        //        }

        //        using (var webClient = new WebClient())
        //        {
        //            var imageBytes = webClient.DownloadData(url);

        //            // Check if the file is a image
        //            try
        //            {
        //                var image = new WebImage(imageBytes);
        //                if (AllowedImageFormats.All(aif => aif != image.ImageFormat))
        //                {
        //                    throw new DomainException("The image format from url " + url + " must be jpeg, jpg, gif or png.");
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw new DomainException("The file from url " + url + " is not an image.");
        //            }

        //            return webClient.DownloadData(url);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        throw new DomainException("The image could not be opened from url " + url + ".");
        //    }
        //}

        #region Private Methods

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