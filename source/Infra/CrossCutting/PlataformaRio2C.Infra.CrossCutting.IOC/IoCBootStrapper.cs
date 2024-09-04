// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.IOC
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="IoCBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LazyCache;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;
using System;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms;
using System.Data.Entity;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    /// <summary>IoCBootStrapper</summary>
    public static class IoCBootStrapper
    {
        /// <summary>Registers the services.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterServices(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            // Configures PlataformaRio2C.Infra.Data.Context
            container.Register<PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<Data.Context.Interfaces.IUnitOfWork, Data.Context.Models.UnitOfWorkWithLog<Data.Context.PlataformaRio2CContext>>(Lifestyle.Scoped);
            container.Register<ILogService>(() => new LogService(true), Lifestyle.Scoped);

            // Configures PlataformaRio2C.Infra.CrossCutting.Identity
            container.Register<PlataformaRio2C.Infra.CrossCutting.Identity.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser, int>>(() => new CustomUserStore<ApplicationUser>(container.GetInstance<Identity.Context.PlataformaRio2CContext>()), Lifestyle.Scoped);
            container.Register<IRoleStore<CustomRole, int>>(() => new CustomRoleStore<ApplicationUser>(container.GetInstance<Identity.Context.PlataformaRio2CContext>()), Lifestyle.Scoped);
            container.Register(() => new ApplicationUserManager<ApplicationUser>(container.GetInstance<IUserStore<ApplicationUser, int>>(), container.GetInstance<IdentityServicesSetup>()), Lifestyle.Scoped);
            container.Register<ApplicationSignInManager<ApplicationUser>>(Lifestyle.Scoped);
            container.Register<IdentityAutenticationService>(Lifestyle.Scoped);
            container.Register(() => MakeIdentityServicesSetup(container), Lifestyle.Scoped);

            // Configures PlataformaRio2C.Infra.CrossCutting.SystemParameter
            container.Register<ISalesPlatformServiceFactory, SalesPlatformServiceFactory>(Lifestyle.Scoped);
            container.Register<ISocialMediaPlatformServiceFactory, SocialMediaPlatformServiceFactory>(Lifestyle.Scoped);
            container.Register<PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<IUnitOfWorkSystemParameter, UnitOfWorkWithLog<PlataformaRio2CContext>>(Lifestyle.Scoped);

            RegisterRepositories(container);
        }

        /// <summary>Registers the repositories.</summary>
        /// <param name="container">The container.</param>
        private static void RegisterRepositories(Container container)
        {
            var repositoryAssembly = typeof(UserRepository).Assembly;
            var registrations = from type in repositoryAssembly.GetExportedTypes()
                                where type.Namespace.StartsWith("PlataformaRio2C.Infra.Data.Repository.Repositories")
                                from service in type.GetInterfaces()
                                select new { service, implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.service, reg.implementation, Lifestyle.Scoped);
            }
        }

        /// <summary>Makes the identity services setup.</summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        private static IdentityServicesSetup MakeIdentityServicesSetup(Container container)
        {
            IAppCache cache = new CachingService();

            var setup = cache.Get<IdentityServicesSetup>("MakeIdentityServicesSetup-setup");

            setup = new IdentityServicesSetup
            {
                EmailSetup = new IdentityEmailSetup(),
                SmsSetup = new IdentitySmsSetup()
            };

            return setup;
        }
    }
}