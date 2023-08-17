// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2023
// ***********************************************************************
// <copyright file="FileLocalRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.IO;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;

namespace PlataformaRio2c.Infra.Data.FileRepository
{
    /// <summary>FileLocalRepository</summary>
    public class FileLocalRepository : IFileRepository
    {
        private readonly string localBucket;
        private readonly string imagesHoldingsDirectory;
        private readonly string imagesOrganizationsDirectory;
        private readonly string imagesMusicBandsDirectory;
        private readonly string imagesUsersDirectory;
        private readonly string filesLogisticsAirfareDirectory;
        private readonly string filesInnovationOrganizationsDirectory;
        private readonly string audioFilesDirectory;
        private readonly string weConnectMediaFilesDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLocalRepository"/> class.
        /// </summary>
        public FileLocalRepository()
        {
            this.localBucket = ConfigurationManager.AppSettings["LocalBucket"];
            this.imagesHoldingsDirectory = ConfigurationManager.AppSettings["LocalImagesHoldingsDirectory"];
            this.imagesOrganizationsDirectory = ConfigurationManager.AppSettings["LocalImagesOrganizationsDirectory"];
            this.imagesMusicBandsDirectory = ConfigurationManager.AppSettings["LocalImagesMusicBandsDirectory"];
            this.imagesUsersDirectory = ConfigurationManager.AppSettings["LocalImagesUsersDirectory"];
            this.filesLogisticsAirfareDirectory = ConfigurationManager.AppSettings["LocalFilesLogisticsAirfareDirectory"];
            this.filesInnovationOrganizationsDirectory = ConfigurationManager.AppSettings["LocalFilesInnovationOrganizationsDirectory"];
            this.audioFilesDirectory = ConfigurationManager.AppSettings["AwsAudioFilesDirectory"];
            this.weConnectMediaFilesDirectory = ConfigurationManager.AppSettings["LocalWeConnectMediaFilesDirectory"];
        }

        #region Get Url

        /// <summary>Gets the image URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="imageUid">The image uid.</param>
        /// <param name="imageUploadDate">The image upload date.</param>
        /// <param name="isThumbnail">if set to <c>true</c> [is thumbnail].</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        /// <returns></returns>
        public string GetImageUrl(FileRepositoryPathType fileRepositoryPathType, Guid? imageUid, DateTimeOffset? imageUploadDate, bool isThumbnail, string additionalFileInfo)
        {
            if (!imageUploadDate.HasValue || !imageUid.HasValue)
            {
                return string.Empty;
            }

            return this.GetUrl(fileRepositoryPathType, imageUid.Value) + (isThumbnail ? $"_thumbnail{additionalFileInfo}.png" : $"_original{additionalFileInfo}.png") + $"?v={imageUploadDate.Value.ToString("yyyyMMddHHmmss")}";
        }

        /// <summary>Gets the file URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="objectUid">The object uid.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        /// <returns></returns>
        public string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, Guid? objectUid, DateTimeOffset? uploadDate, string additionalFileInfo = null, string fileExtension = null)
        {
            if (!uploadDate.HasValue || !objectUid.HasValue)
            {
                return string.Empty;
            }

            if(string.IsNullOrEmpty(fileExtension))
            {
                fileExtension = FileType.Pdf;
            }

            return this.GetUrl(fileRepositoryPathType, objectUid.Value) + $"{additionalFileInfo}{fileExtension}" + $"?v={uploadDate.Value.ToString("yyyyMMddHHmmss")}";
        }

        /// <summary>
        /// Gets the file URL.
        /// </summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="additionalFileInfo">The additional file information.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        public string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, string fileName, string additionalFileInfo = null, string fileExtension = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(fileExtension))
            {
                fileExtension = FileType.Pdf;
            }

            return this.GetUrl(fileRepositoryPathType, fileName) + $"{additionalFileInfo}{fileExtension}";
        }

        /// <summary>Gets the URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="fileUid">The file uid.</param>
        /// <returns></returns>
        public string GetUrl(FileRepositoryPathType fileRepositoryPathType, Guid fileUid)
        {
            return this.GetDirectoryUrl(fileRepositoryPathType) + fileUid;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string GetUrl(FileRepositoryPathType fileRepositoryPathType, string fileName)
        {
            return this.GetDirectoryUrl(fileRepositoryPathType) + fileName;
        }

        /// <summary>Gets the base URL.</summary>
        /// <returns></returns>
        public string GetBaseUrl()
        {
            return "/" + this.localBucket + "/";
        }

        #endregion

        #region Upload

        /// <summary>Uploads the specified input stream.</summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        public void Upload(Stream inputStream, string contentType, string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            // Get the phisical directory
            var directory = this.GetBaseDirectory(fileRepositoryPathType, args);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fileStream = File.Create(directory + fileName))
            {
                inputStream.CopyTo(fileStream);
            }
        }

        #endregion

        #region Delete

        /// <summary>Deletes the images.</summary>
        /// <param name="imageUid">The image uid.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        public void DeleteImages(Guid imageUid, FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            var fileName = imageUid + "*.png";

            this.DeleteFiles(fileName, fileRepositoryPathType, args);
        }

        /// <summary>Deletes the files.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        public void DeleteFiles(string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            var directory = this.GetBaseDirectory(fileRepositoryPathType, args);
            File.Delete(directory + fileName);
        }

        #endregion

        #region Private Methods

        /// <summary>Gets the directory URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private string GetDirectoryUrl(FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            return this.GetBaseUrl() + "/" + this.GetBaseDirectory(fileRepositoryPathType, args);
        }

        /// <summary>Gets the base directory.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private string GetBaseDirectory(FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            if (fileRepositoryPathType.Uid == FileRepositoryPathType.HoldingImage.Uid)
            {
                return string.Format(this.imagesHoldingsDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.OrganizationImage.Uid)
            {
                return string.Format(this.imagesOrganizationsDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.MusicBandImage.Uid)
            {
                return string.Format(this.imagesMusicBandsDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.UserImage.Uid)
            {
                return string.Format(this.imagesUsersDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.LogisticAirfareFile.Uid)
            {
                return string.Format(this.filesLogisticsAirfareDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.InnovationOrganizationPresentationFile.Uid)
            {
                return string.Format(this.filesInnovationOrganizationsDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.AudioFile.Uid)
            {
                return string.Format(this.audioFilesDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.WeConnectMediaFile.Uid)
            {
                return string.Format(this.weConnectMediaFilesDirectory, args);
            }

            return string.Empty;
        }

        #endregion
    }
}
