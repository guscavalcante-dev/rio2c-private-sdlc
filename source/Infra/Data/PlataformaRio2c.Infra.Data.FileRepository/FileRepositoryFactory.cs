// ***********************************************************************
// Assembly         : PlataformaRio2c.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="FileRepositoryFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Configuration;

namespace PlataformaRio2c.Infra.Data.FileRepository
{
    /// <summary>FileRepositoryFactory</summary>
    public class FileRepositoryFactory : IFileRepositoryFactory
    {
        // Repository
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepositoryFactory"/> class.
        /// </summary>
        public FileRepositoryFactory()
        {
            switch (ConfigurationManager.AppSettings["FileHost"])
            {
                // Amazon Web Services
                case "aws":
                    this.fileRepo = new FileAwsRepository();
                    break;

                // Local server repository
                case "local":
                    this.fileRepo = new FileLocalRepository();
                    break;

                default:
                    this.fileRepo = new FileLocalRepository();
                    break;
            }
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>IFileRepository.</returns>
        public IFileRepository Get()
        {
            return this.fileRepo;
        }
    }
}