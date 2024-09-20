// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.IOC
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="HubBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using PlataformaRio2C.HubApplication.CQRS.CommandsHandlers;
using PlataformaRio2C.Infra.CrossCutting.CQRS;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    /// <summary>HubBootStrapper</summary>
    public static class HubBootStrapper
    {
        /// <summary>Initializes the thread scoped.</summary>
        /// <returns></returns>
        public static Container InitializeThreadScoped()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            ResolveBase(container);
            RegisterRepositories(container);
            CqrsBootStrapper.RegisterServices(container, new[]
            {
                typeof(CreateMessageCommandHandler).Assembly
            });

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

        /// <summary>Resolves the base.</summary>
        /// <param name="container">The container.</param>
        public static void ResolveBase(Container container)
        {
            container.Register<Infra.Data.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<ILogService>(() => new LogService(true), Lifestyle.Scoped);
            container.Register<Infra.Data.Context.Interfaces.IUnitOfWork, Infra.Data.Context.Models.UnitOfWorkWithLog<Infra.Data.Context.PlataformaRio2CContext>>(Lifestyle.Scoped);
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
    }
}