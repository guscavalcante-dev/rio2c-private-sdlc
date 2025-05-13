// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2021
// ***********************************************************************
// <copyright file="IFileRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Statics;
using System;
using System.IO;

namespace PlataformaRio2c.Infra.Data.FileRepository
{
    /// <summary>IFileRepository</summary>
    public interface IFileRepository
    {
        string GetImageUrl(FileRepositoryPathType fileRepositoryPathType, Guid? imageUid, DateTimeOffset? imageUploadDate, bool isThumbnail, string additionalFileInfo = null);
        string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, Guid? objectUid, DateTimeOffset? uploadDate, string additionalFileInfo = null, string fileExtension = null);
        string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, string fileName, string additionalFileInfo = null, string fileExtension = null);
        string GetUrl(FileRepositoryPathType fileRepositoryPathType, Guid fileUid);
        void Upload(Stream inputStream, string contentType, string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args);
        void DeleteImages(Guid imageUid, FileRepositoryPathType fileRepositoryPathType, params object[] args);
        void DeleteFiles(string fileName, FileRepositoryPathType fileRepositoryPathType, params object[] args);
        string GetBaseUrl();
    }
}
