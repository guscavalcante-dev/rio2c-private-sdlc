// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
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

            return this.GetUrl(fileRepositoryPathType, objectUid.Value) + $"{additionalFileInfo}{fileExtension}" + $"?v={uploadDate.Value.ToString("yyyyMMddHHmmss")}";
        }

        /// <summary>Gets the URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file.</param>
        /// <param name="fileUid">The file uid.</param>
        /// <returns></returns>
        public string GetUrl(FileRepositoryPathType fileRepositoryPathType, Guid fileUid)
        {
            return this.GetDirectoryUrl(fileRepositoryPathType) + fileUid;
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

            return string.Empty;
        }

        /// <summary>Gets the base URL.</summary>
        /// <returns></returns>
        private string GetBaseUrl()
        {
            if (string.IsNullOrEmpty(this.awsBucket))
            {
                return string.Empty;
            }

            return $"https://{this.awsBucket}";
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

        ///// <summary>
        ///// Determines whether the specified identifier has avatar.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //public bool HasAvatar(int id)
        //{
        //    // GetLink
        //    var link = this.GetLink("avatar" + id.ToString(CultureInfo.CurrentCulture) + ".", "avatar");

        //    return link != null;
        //}

        ///// <summary>
        ///// Determines whether [has avatar copy] [the specified identifier].
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //public bool HasAvatarCopy(int id)
        //{
        //    // GetLink
        //    var link = this.GetLink("copy-avatar" + id.ToString(CultureInfo.CurrentCulture) + ".", "avatar");

        //    return link != null;
        //}

        ///// <summary>
        ///// Gets the avatar link.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="personTypeId">The person type id.</param>
        ///// <returns>System.String.</returns>
        //public string GetLinkForAvatar(int id, int personTypeId)
        //{
        //    // GetLink
        //    var link = this.GetLink("avatar" + id.ToString(CultureInfo.CurrentCulture) + ".", "avatar");

        //    // Return default avatar in case of no file found
        //    if (link == null)
        //    {
        //        if (personTypeId == Domain.Statics.PersonType.Individual.Id)
        //        {
        //            return "/Content/img/persons/no_avatar_individual.png";
        //        }

        //        return "/Content/img/persons/no_avatar_company.png";
        //    }

        //    return link;
        //}

        ///// <summary>
        ///// Gets the link.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id"></param>
        ///// <returns>
        ///// System.String.
        ///// </returns>
        //public string GetLink(string fileName, string fileType, string id = null)
        //{
        //    // Get the phisical directory
        //    var directory = this.GetPhysicalDirectory(fileType, id);

        //    var files = this.GetFiles(directory, fileName);
        //    if (files == null || files.Count == 0)
        //    {
        //        return null;
        //    }

        //    string url;
        //    try
        //    {
        //        using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //        {
        //            // simple object put
        //            var request = new GetPreSignedUrlRequest
        //            {
        //                BucketName = this.awsBucket,
        //                Key = files[0],
        //                Expires = DateTime.UtcNow.AddHours(24)
        //            };

        //            url = client.GetPreSignedURL(request);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // Return null if the file does not exist or other error occured
        //        return null;
        //    }

        //    return url;
        //}

        ///// <summary>
        ///// Gets the specified file name.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id"></param>
        ///// <returns>
        ///// System.String.
        ///// </returns>
        ///// <exception cref="System.IO.FileNotFoundException"></exception>
        //public MemoryStream Get(string fileName, string fileType, string id = null)
        //{
        //    // Get the phisical directory
        //    var directory = this.GetPhysicalDirectory(fileType, id);

        //    var files = this.GetFiles(directory, fileName);
        //    if (files == null || files.Count == 0)
        //    {
        //        return null;
        //    }

        //    MemoryStream ms;
        //    using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //    {
        //        // simple object put
        //        var request = new GetObjectRequest
        //        {
        //            BucketName = this.awsBucket,
        //            Key = files[0],
        //        };

        //        var response = client.GetObject(request);
        //        var stream = response.ResponseStream;
        //        ms = new MemoryStream();
        //        stream.CopyTo(ms);
        //        ms.Seek(0, 0);
        //    }

        //    return ms;
        //}

        ///// <summary>
        ///// Uploads the specified sent file.
        ///// </summary>
        ///// <param name="inputStream">The input stream.</param>
        ///// <param name="contentType">Type of the content.</param>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id"></param>
        //public void Upload(Stream inputStream, string contentType, string fileName, string fileType, string id = null)
        //{
        //    // Get the phisical directory
        //    var directory = this.GetPhysicalDirectory(fileType, id);

        //    try
        //    {
        //        using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //        {
        //            // simple object put
        //            var request = new PutObjectRequest
        //            {
        //                Key = directory + fileName,
        //                BucketName = this.awsBucket,
        //                InputStream = inputStream,
        //                ContentType = contentType
        //            };

        //            //S3Response response = client.PutObject(request);
        //            client.PutObject(request);
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //             amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //            throw;
        //        }

        //        Console.WriteLine("An error occurred with the message '{0}' when writing an object",
        //            amazonS3Exception.Message);
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Gets the directory.
        ///// </summary>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id"></param>
        ///// <returns>System.String.</returns>
        //public string GetPhysicalDirectory(string fileType, string id = null)
        //{
        //    if (fileType == Domain.Statics.FileType.Avatar.Tag)
        //    {
        //        return this.avatarDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.HelpAttachment.Tag)
        //    {
        //        return this.helpAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalAttachment.Tag && id != null)
        //    {
        //        return this.proposalAttachDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAttachment.Tag && id != null)
        //    {
        //        return this.projectAttachDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.CustomerProjectAttachment.Tag && id != null)
        //    {
        //        return this.projectAttachCustomerDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.KnowledgeFile.Tag)
        //    {
        //        return this.knowledgeFileDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalDocumentTemplateAttachment.Tag)
        //    {
        //        return this.proposalDocumentTemplateAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.HorizontalCashFlowReportExecutionAttachment.Tag)
        //    {
        //        return this.horizontalCashFlowReportExecutionAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.EmployeeFile.Tag)
        //    {
        //        return this.employeeFileDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAcceptanceTemplate.Tag)
        //    {
        //        return this.projectAcceptanceTemplateDirectory;
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// Gets a list of files in the specified directory.
        ///// </summary>
        ///// <param name="directory">The directory path.</param>
        ///// <param name="filename"></param>
        ///// <returns></returns>
        //public List<string> GetFiles(string directory, string filename = null)
        //{
        //    var files = new List<string>();
        //    using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //    {
        //        var request = new ListObjectsRequest
        //        {
        //            BucketName = this.awsBucket
        //        };

        //        if (filename != null)
        //            request.Prefix = directory + filename;
        //        else
        //            request.Prefix = directory;

        //        do
        //        {
        //            ListObjectsResponse response = client.ListObjects(request);

        //            files.AddRange(response.S3Objects.Select(entry => entry.Key));

        //            if (response.IsTruncated)
        //            {
        //                request.Marker = response.NextMarker;
        //            }
        //            else
        //            {
        //                request = null;
        //            }
        //        }
        //        while (request != null);
        //    }
        //    return files;
        //}

        ///// <summary>
        ///// Deletes the file.
        ///// </summary>
        ///// <param name="file">The file.</param>
        ///// <param name="fileType">The file type.</param>
        //public void DeleteFile(string file, string fileType)
        //{
        //    try
        //    {
        //        using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //        {
        //            // Create a DeleteObject request
        //            var request = new DeleteObjectRequest
        //            {
        //                BucketName = this.awsBucket,
        //                Key = file
        //            };

        //            // Issue request
        //            client.DeleteObject(request);
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //             amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //            throw;
        //        }

        //        Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Deletes the file all types.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        //public void DeleteFileAllTypes(string fileName, string fileType, string id = null)
        //{
        //    var directory = this.GetPhysicalDirectory(fileType, id);
        //    foreach (string file in this.GetFiles(directory, fileName))
        //    {
        //        this.DeleteFile(file, fileType);
        //    }
        //}

        ///// <summary>
        ///// Deletes the old avatar temporary files.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        //public void DeleteOldFileAllTypes(string fileName, string fileType, string id = null)
        //{
        //    var directory = this.GetPhysicalDirectory(fileType, id);
        //    foreach (string file in this.GetFiles(directory, fileName))
        //    {
        //        var lastModifiedDate = this.GetFileDate(file);
        //        if (lastModifiedDate <= DateTime.Now.AddDays(-1))
        //        {
        //            this.DeleteFile(file, fileType);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Duplicates the file.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        //public void DuplicateFile(string fileName, string fileType)
        //{
        //    // Get the phisical directory
        //    var directory = GetPhysicalDirectory(fileType);

        //    var files = this.GetFiles(directory, fileName);
        //    if (files == null || files.Count == 0)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //        {
        //            var request = new CopyObjectRequest
        //            {
        //                SourceBucket = this.awsBucket,
        //                SourceKey = files[0],
        //                DestinationBucket = this.awsBucket,
        //                DestinationKey = directory + "copy-" + Path.GetFileName(files[0])
        //            };
        //            var response = client.CopyObject(request);
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //             amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //            throw;
        //        }

        //        Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Gets the file date.
        ///// </summary>
        ///// <param name="fileNameWithDirectory">The file name with directory.</param>
        ///// <returns></returns>
        //private DateTime GetFileDate(string fileNameWithDirectory)
        //{
        //    try
        //    {
        //        using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(this.awsAccessKey, this.awsSecretKey, Amazon.RegionEndpoint.SAEast1))
        //        {
        //            GetObjectRequest request = new GetObjectRequest
        //            {
        //                BucketName = this.awsBucket,
        //                Key = fileNameWithDirectory
        //            };

        //            using (GetObjectResponse response = client.GetObject(request))
        //            {
        //                return response.LastModified;
        //            }
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //             amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //            throw;
        //        }

        //        Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
        //        throw;
        //    }
        //}
    }
}
