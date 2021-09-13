// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
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

        /// <summary>Gets the URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
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

            return string.Empty;
        }

        /// <summary>Gets the base URL.</summary>
        /// <returns></returns>
        private string GetBaseUrl()
        {
            return "/" + this.localBucket + "/";
        }

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
        ///// <returns>
        ///// System.String.
        ///// </returns>
        //public string GetLinkForAvatar(int id, int personTypeId)
        //{
        //    // GetLink
        //    var link = this.GetLink("avatar" + id.ToString(CultureInfo.CurrentCulture) + ".", "avatar");

        //    // Return default avatar in case of no file found
        //    if (link == null)
        //    {
        //        if (personTypeId == Domain.Statics.PersonType.Individual.Id)
        //        {
        //            return this.GetVirtualDirectory("avatar") + "no_avatar_individual.png";
        //        }

        //        return this.GetVirtualDirectory("avatar") + "no_avatar_company.png";
        //    }

        //    return link;
        //}

        ///// <summary>
        ///// Gets the link.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        ///// <returns>
        ///// System.String.
        ///// </returns>
        //public string GetLink(string fileName, string fileType, string id = null)
        //{
        //    var directory = this.GetVirtualDirectory(fileType, id);

        //    var files = this.GetFiles(directory, fileName);
        //    if (files == null || files.Count == 0)
        //    {
        //        return null;
        //    }

        //    return directory + Path.GetFileName(files[0]);
        //}

        ///// <summary>
        ///// Gets the specified file name.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        ///// <returns>
        ///// MemoryStream.
        ///// </returns>
        //public MemoryStream Get(string fileName, string fileType, string id = null)
        //{
        //    // Get the phisical directory
        //    var physicalDirectory = this.GetPhysicalDirectory(fileType, id);
        //    var virtualDirectory = this.GetVirtualDirectory(fileType, id);
        //    if (string.IsNullOrEmpty(physicalDirectory) || string.IsNullOrEmpty(virtualDirectory))
        //    {
        //        return null;
        //    }

        //    var files = this.GetFiles(virtualDirectory, fileName);
        //    if (files == null || files.Count == 0)
        //    {
        //        return null;
        //    }

        //    MemoryStream ms;
        //    try
        //    {
        //        using (var file = new FileStream(files[0], FileMode.Open, FileAccess.Read))
        //        {
        //            var bytes = new byte[file.Length];
        //            file.Read(bytes, 0, (int)file.Length);

        //            ms = new MemoryStream();
        //            ms.Write(bytes, 0, (int)file.Length);
        //            ms.Seek(0, 0);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new FileNotFoundException();
        //    }

        //    return ms;
        //}

        ///// <summary>
        ///// Uploads the specified input stream.
        ///// </summary>
        ///// <param name="inputStream">The input stream.</param>
        ///// <param name="contentType">Type of the content.</param>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        //public void Upload(Stream inputStream, string contentType, string fileName, string fileType, string id = null)
        //{
        //    // Get the phisical directory
        //    var directory = this.GetPhysicalDirectory(fileType, id);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    using (var fileStream = File.Create(directory + fileName))
        //    {
        //        inputStream.CopyTo(fileStream);
        //    }
        //}

        ///// <summary>
        ///// Gets the directory.
        ///// </summary>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id"></param>
        ///// <returns>
        ///// System.String.
        ///// </returns>
        //public string GetPhysicalDirectory(string fileType, string id = null)
        //{
        //    if (fileType == Domain.Statics.FileType.Avatar.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.avatarDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.HelpAttachment.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.helpAttachDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalAttachment.Tag && id != null)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.proposalAttachDirectory.FormatWith(id));
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAttachment.Tag && id != null)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.projectAttachDirectory.FormatWith(id));
        //    }
        //    if (fileType == Domain.Statics.FileType.CustomerProjectAttachment.Tag && id != null)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.projectAttachCustomerDirectory.FormatWith(id));
        //    }
        //    if (fileType == Domain.Statics.FileType.KnowledgeFile.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.knowledgeFileDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalDocumentTemplateAttachment.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.proposalDocumentTemplateAttachDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.HorizontalCashFlowReportExecutionAttachment.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.horizontalCashFlowReportExecutionAttachDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.EmployeeFile.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.employeeFileDirectory.FormatWith(id));
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAcceptanceTemplate.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.projectAcceptanceTemplateDirectory);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalTempFile.Tag)
        //    {
        //        return Path.Combine(HttpRuntime.AppDomainAppPath, this.proposalTemp);
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// Gets the link directory.
        ///// </summary>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //public string GetVirtualDirectory(string fileType, string id = null)
        //{
        //    if (fileType == Domain.Statics.FileType.Avatar.Tag)
        //    {
        //        return "/" + this.avatarDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.HelpAttachment.Tag)
        //    {
        //        return "/" + this.helpAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalAttachment.Tag && id != null)
        //    {
        //        return "/" + this.proposalAttachDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAttachment.Tag && id != null)
        //    {
        //        return "/" + this.projectAttachDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.CustomerProjectAttachment.Tag && id != null)
        //    {
        //        return "/" + this.projectAttachCustomerDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.KnowledgeFile.Tag)
        //    {
        //        return "/" + this.knowledgeFileDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalDocumentTemplateAttachment.Tag)
        //    {
        //        return "/" + this.proposalDocumentTemplateAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.HorizontalCashFlowReportExecutionAttachment.Tag)
        //    {
        //        return "/" + this.horizontalCashFlowReportExecutionAttachDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.EmployeeFile.Tag)
        //    {
        //        return "/" + this.employeeFileDirectory.FormatWith(id);
        //    }
        //    if (fileType == Domain.Statics.FileType.ProjectAcceptanceTemplate.Tag)
        //    {
        //        return "/" + this.projectAcceptanceTemplateDirectory;
        //    }
        //    if (fileType == Domain.Statics.FileType.ProposalTempFile.Tag)
        //    {
        //        return "/" + this.proposalTemp;
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// Gets the files.
        ///// </summary>
        ///// <param name="directory">The directory.</param>
        ///// <param name="filename">The filename.</param>
        ///// <returns>
        ///// List{System.String}.
        ///// </returns>
        //public List<string> GetFiles(string directory, string filename = null)
        //{
        //    var files = new List<string>();
        //    try
        //    {
        //        if (filename != null)
        //        {
        //            files.AddRange(Directory.GetFiles(HttpContext.Current.Server.MapPath(directory), filename + "*"));
        //        }
        //        else
        //        {
        //            files.AddRange(Directory.GetFiles(HttpContext.Current.Server.MapPath(directory)));
        //        }
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        return new List<string>();
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
        //    var directory = this.GetPhysicalDirectory(fileType);
        //    File.Delete(directory + file);
        //}

        ///// <summary>
        ///// Deletes the file all types.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <param name="id">The identifier.</param>
        //public void DeleteFileAllTypes(string fileName, string fileType, string id = null)
        //{
        //    var directory = this.GetVirtualDirectory(fileType, id);
        //    if (Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
        //    {
        //        foreach (string file in this.GetFiles(directory, fileName))
        //        {
        //            this.DeleteFile(file.Replace(HttpContext.Current.Server.MapPath(directory), ""), fileType);
        //        }
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
        //    var directory = this.GetVirtualDirectory(fileType, id);
        //    foreach (string file in this.GetFiles(directory, fileName))
        //    {
        //        var fileDate = this.GetFileDate(file);
        //        if (fileDate <= DateTime.Now.AddDays(-1))
        //        {
        //            this.DeleteFile(file.Replace(HttpContext.Current.Server.MapPath(directory), ""), fileType);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Duplicates the file.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileType">Type of the file.</param>
        ///// <exception cref="System.Exception">The directory for the file type ( + fileType + ) does not exist.</exception>
        //public void DuplicateFile(string fileName, string fileType)
        //{
        //    var physicalDirectory = this.GetPhysicalDirectory(fileType);
        //    var virtualDirectory = this.GetVirtualDirectory(fileType);
        //    if (string.IsNullOrEmpty(physicalDirectory) || string.IsNullOrEmpty(virtualDirectory))
        //    {
        //        throw new Exception("The directory for the file type (" + fileType + ") does not exist.");
        //    }

        //    var files = this.GetFiles(virtualDirectory, fileName);
        //    if (files == null || !files.Any())
        //    {
        //        return;
        //    }

        //    File.Copy(files[0], Path.Combine(physicalDirectory, "copy-" + Path.GetFileName(files[0])));
        //}

        ///// <summary>
        ///// Gets the file date.
        ///// </summary>
        ///// <param name="fileNameWithDirectory">The file name with directory.</param>
        ///// <returns></returns>
        //private DateTime GetFileDate(string fileNameWithDirectory)
        //{
        //    return System.IO.File.GetCreationTime(fileNameWithDirectory);
        //}
    }
}
