// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
// ***********************************************************************
// <copyright file="IFileRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using PlataformaRio2C.Domain.Statics;

namespace PlataformaRio2c.Infra.Data.FileRepository
{
    /// <summary>IFileRepository</summary>
    public interface IFileRepository
    {
        string GetImageUrl(FileRepositoryPathType fileRepositoryPathType, Guid? imageUid, DateTimeOffset? imageUploadDate, bool isThumbnail, string additionalFileInfo = null);
        string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, Guid? objectUid, DateTimeOffset? uploadDate, string additionalFileInfo = null);
        string GetUrl(FileRepositoryPathType fileRepositoryPathType, Guid fileUid);
        void Upload(Stream inputStream, string contentType, string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args);
        void DeleteImages(Guid imageUid, FileRepositoryPathType fileRepositoryPathType, params object[] args);

        //string GetLinkForAvatar(int id, int personTypeId = 1);
        //bool HasAvatar(int id);
        //bool HasAvatarCopy(int id);
        //string GetLink(string fileName, string fileType, string id = null);
        //MemoryStream Get(string fileName, string fileType, string id = null);
        //void Upload(Stream inputStream, string contentType, string fileName, string fileType, string id = null);
        //List<string> GetFiles(string directory, string filename = null);
        //string GetPhysicalDirectory(string fileType, string id = null);
        //void DeleteFile(string file, string fileType);
        //void DeleteFileAllTypes(string fileName, string fileType, string id = null);
        //void DeleteOldFileAllTypes(string fileName, string fileType, string id = null);
        //void DuplicateFile(string fileName, string fileType);
    }
}
