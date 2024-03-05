// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2023
// ***********************************************************************
// <copyright file="FileAwsRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;

namespace PlataformaRio2c.Infra.Data.FileRepository
{
    /// <summary>FileAwsRepository</summary>
    public class FileAwsRepository : IFileRepository
    {
        private readonly string awsAccessKey;
        private readonly string awsSecretKey;
        private readonly string awsBucket;
        private readonly string imagesHoldingsDirectory;
        private readonly string imagesOrganizationsDirectory;
        private readonly string imagesMusicBandsDirectory;
        private readonly string imagesUsersDirectory;
        private readonly string filesLogisticsAirfareDirectory;
        private readonly string filesInnovationOrganizationsDirectory;
        private readonly string audioFilesDirectory;
        private readonly string weConnectMediaFilesDirectory;
        private readonly string creatorProjectsFilesDirectory;

        private readonly string errorCroppingDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAwsRepository"/> class.
        /// </summary>
        public FileAwsRepository()
        {
            this.awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            this.awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            this.awsBucket = ConfigurationManager.AppSettings["AWSBucket"];
            this.imagesHoldingsDirectory = ConfigurationManager.AppSettings["AwsImagesHoldingsDirectory"];
            this.imagesOrganizationsDirectory = ConfigurationManager.AppSettings["AwsImagesOrganizationsDirectory"];
            this.imagesMusicBandsDirectory = ConfigurationManager.AppSettings["AwsImagesMusicBandsDirectory"];
            this.imagesUsersDirectory = ConfigurationManager.AppSettings["AwsImagesUsersDirectory"];
            this.filesLogisticsAirfareDirectory = ConfigurationManager.AppSettings["AwsFilesLogisticsAirfareDirectory"];
            this.filesInnovationOrganizationsDirectory = ConfigurationManager.AppSettings["AwsFilesInnovationOrganizationsDirectory"];
            this.audioFilesDirectory = ConfigurationManager.AppSettings["AwsAudioFilesDirectory"];
            this.weConnectMediaFilesDirectory = ConfigurationManager.AppSettings["AwsWeConnectMediaFilesDirectory"];
            this.creatorProjectsFilesDirectory = ConfigurationManager.AppSettings["AwsFilesCreatorProjectsDirectory"];

            this.errorCroppingDirectory = "img/errorCropping/";
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

            if (string.IsNullOrEmpty(fileExtension))
            {
                fileExtension = FileType.Pdf;
            }

            if (!fileExtension.StartsWith("."))
            {
                fileExtension = $".{fileExtension}";
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
        /// <param name="fileRepositoryPathType">Type of the file.</param>
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
            if (string.IsNullOrEmpty(this.awsBucket))
            {
                return string.Empty;
            }

            return $"https://{this.awsBucket}";
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
            var directory = this.GetBaseDirectory(fileRepositoryPathType, args);

            try
            {
                using (IAmazonS3 client = new AmazonS3Client(this.awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                {
                    // simple object put
                    var request = new PutObjectRequest
                    {
                        Key = directory + fileName,
                        BucketName = awsBucket,
                        InputStream = inputStream,
                        ContentType = contentType
                    };

                    //S3Response response = client.PutObject(request);
                    client.PutObject(request);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                     amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    throw;
                }

                Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                throw;
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
            var fileName = imageUid + "_";

            this.DeleteFiles(fileName, fileRepositoryPathType, args);
        }

        /// <summary>Deletes the files.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="args">The arguments.</param>
        public void DeleteFiles(string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args)
        {
            var directory = this.GetBaseDirectory(fileRepositoryPathType, args);

            foreach (var file in this.GetFiles(directory, fileName))
            {
                this.DeleteFile(file);
            }
        }

        #endregion

        #region Private Methods

        #region Directories

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

            if(fileRepositoryPathType.Uid == FileRepositoryPathType.ErrorCropping.Uid)
            {
                return string.Format(this.errorCroppingDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.WeConnectMediaFile.Uid)
            {
                return string.Format(this.weConnectMediaFilesDirectory, args);
            }

            if (fileRepositoryPathType.Uid == FileRepositoryPathType.CreatorProjectFile.Uid)
            {
                return string.Format(this.creatorProjectsFilesDirectory, args);
            }

            return string.Empty;
        }

        #endregion

        #region Get

        /// <summary>Gets the files.</summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private List<string> GetFiles(string directory, string filename = null)
        {
            var files = new List<string>();
            using (IAmazonS3 client = new AmazonS3Client(this.awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
            {
                var request = new ListObjectsRequest
                {
                    BucketName = this.awsBucket
                };

                if (filename != null)
                    request.Prefix = directory + filename;
                else
                    request.Prefix = directory;

                do
                {
                    ListObjectsResponse response = client.ListObjects(request);

                    files.AddRange(response.S3Objects.Select(entry => entry.Key));

                    if (response.IsTruncated)
                    {
                        request.Marker = response.NextMarker;
                    }
                    else
                    {
                        request = null;
                    }
                }
                while (request != null);
            }
            return files;
        }

        #endregion

        #region Delete

        /// <summary>Deletes the file.</summary>
        /// <param name="fileName">Name of the file.</param>
        private void DeleteFile(string fileName)
        {
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(this.awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                {
                    // Create a DeleteObject request
                    var request = new DeleteObjectRequest
                    {
                        BucketName = this.awsBucket,
                        Key = fileName
                    };

                    // Issue request
                    client.DeleteObject(request);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                     amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    throw;
                }

                Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                throw;
            }
        }

        #endregion

        #endregion
    }
}
