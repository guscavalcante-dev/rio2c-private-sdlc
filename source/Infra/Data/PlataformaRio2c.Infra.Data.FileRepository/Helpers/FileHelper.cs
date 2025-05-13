// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2021
// ***********************************************************************
// <copyright file="FileHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Statics;
using System;

namespace PlataformaRio2c.Infra.Data.FileRepository.Helpers
{
    /// <summary></summary>
    public static class FileHelper
    {
        /// <summary>Gets the file URL.</summary>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="objectUid">The object uid.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <returns></returns>
        public static string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, Guid objectUid, DateTimeOffset? uploadDate, string fileExtension = null)
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return fileRepo.GetFileUrl(fileRepositoryPathType, objectUid, uploadDate, fileExtension: fileExtension);
        }

        public static string GetFileUrl(FileRepositoryPathType fileRepositoryPathType, string fileName, DateTimeOffset? uploadDate, string fileExtension = null)
        {
            var fileRepo = new FileRepositoryFactory().Get();
            return fileRepo.GetFileUrl(fileRepositoryPathType, fileName, fileExtension: fileExtension);
        }
    }
}