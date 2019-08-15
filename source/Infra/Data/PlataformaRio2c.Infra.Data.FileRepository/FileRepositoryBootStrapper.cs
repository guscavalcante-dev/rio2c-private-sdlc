// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.FileRepository
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="FileRepositoryBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2c.Infra.Data.FileRepository;
using SimpleInjector;

namespace PlataformaRio2C.Infra.Data.FileRepository
{
    /// <summary>FileRepositoryBootStrapper</summary>
    public static class FileRepositoryBootStrapper
    {
        /// <summary>Registers the services.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterServices(Container container)
        {
            container.Register<IFileRepository>(() => new FileRepositoryFactory().Get(), Lifestyle.Scoped);
        }
    }    
}