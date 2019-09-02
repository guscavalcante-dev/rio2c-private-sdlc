// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SimpleInjectorInitializer.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Owin;
using PlataformaRio2C.Infra.CrossCutting.IOC;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PlataformaRio2C.Application.CQRS.CommandsHandlers;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Infra.CrossCutting.CQRS;
using PlataformaRio2C.Infra.Data.FileRepository;
using PlataformaRio2C.Web.Admin.Services;

namespace PlataformaRio2C.Web.Admin.App_Start
{
    /// <summary>SimpleInjectorInitializer</summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initializes this instance.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Chamada dos módulos do Simple Injector
            InitializeContainer(container);

            // Necessário para registrar o ambiente do Owin que é dependência do Identity
            // Feito fora da camada de IoC para não levar o System.Web para fora
            container.Register(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying)
                {
                    return new OwinContext().Authentication;
                }
                return HttpContext.Current.GetOwinContext().Authentication;

            }, Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        /// <summary>Initializes the container.</summary>
        /// <param name="container">The container.</param>
        private static void InitializeContainer(Container container)
        {
            IoCBootStrapper.RegisterServices(container);
            AdminIoCBootStrapper.RegisterServices(container);
            FileRepositoryBootStrapper.RegisterServices(container);
            container.Register<IMailerService, MailerService>(Lifestyle.Scoped);
            CqrsBootStrapper.RegisterServices(container, new[]
            {
                typeof(CreateSalesPlatformWebhookRequestCommandHandler).Assembly
            });
        }
    }
}