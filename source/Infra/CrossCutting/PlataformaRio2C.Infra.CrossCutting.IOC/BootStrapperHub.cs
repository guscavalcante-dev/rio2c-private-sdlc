// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.IOC
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="BootStrapperHub.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Services;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Repositories;
using PlataformaRio2C.Infra.Data.Repository;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    /// <summary>BootStrapperHub</summary>
    public static class BootStrapperHub
    {
        /// <summary>Initializes the thread scoped.</summary>
        /// <returns></returns>
        public static Container InitializeThreadScoped()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            container.Register<Data.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Scoped);
            container.Register<ILogService>(() => new LogService(true), Lifestyle.Scoped);
            container.Register<Data.Context.Interfaces.IUnitOfWork, Data.Context.Models.UnitOfWorkWithLog<Data.Context.PlataformaRio2CContext>>(Lifestyle.Scoped);
            container.Register<IMessageAppService, MessageAppService>(Lifestyle.Scoped);
            container.Register<IMessageService, MessageService>(Lifestyle.Scoped);
            container.Register<IMessageRepository, MessageRepository>(Lifestyle.Scoped);
            container.Register<ICollaboratorService, CollaboratorService>(Lifestyle.Scoped);
            container.Register<ICollaboratorRepository, CollaboratorRepository>(Lifestyle.Scoped);
            container.Register<IPlayerRepository, PlayerRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<ILanguageRepository, LanguageRepository>(Lifestyle.Scoped);
            container.Register<IImageFileRepository, ImageFileRepository>(Lifestyle.Scoped);
            container.Register<ICollaboratorJobTitleRepository, CollaboratorJobTitleRepository>(Lifestyle.Scoped);
            container.Register<ICollaboratorMiniBioRepository, CollaboratorMiniBioRepository>(Lifestyle.Scoped);
            container.Register<IUserRoleRepository, UserRoleRepository>(Lifestyle.Scoped);
            container.Register<IRoleRepository, RoleRepository>(Lifestyle.Scoped);
            container.Register<PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<ISystemParameterRepository, SystemParameterRepository>(Lifestyle.Scoped);

            try
            {
                container.Verify();
            }
            catch (Exception e)
            {

                throw;
            }

            return container;
        }
    }
}
