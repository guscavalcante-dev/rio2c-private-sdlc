// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="SimpleInjectorInitializer.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.CQRS;
using PlataformaRio2C.Infra.Data.FileRepository;
using PlataformaRio2C.Web.Site.ServicesSendProducersNegotiationEmail;

namespace PlataformaRio2C.Web.Site
{
    /// <summary>SimpleInjectorInitializer</summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initializes this instance.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            IoCBootStrapper.RegisterServices(container);
            SiteIoCBootStrapper.RegisterServices(container);
            FileRepositoryBootStrapper.RegisterServices(container);
            container.Register<IMailerService, SiteMailerService>(Lifestyle.Scoped);
            CqrsBootStrapper.RegisterServices(container, new[]
            {
                typeof(CreateSalesPlatformWebhookRequestCommandHandler).Assembly
            });

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

            var activator = new SimpleInjectorHubActivator(container);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);
        }
    }

    /// <summary>SimpleInjectorHubActivator</summary>
    public class SimpleInjectorHubActivator : IHubActivator
    {
        private readonly Container _container;

        /// <summary>Initializes a new instance of the <see cref="SimpleInjectorHubActivator"/> class.</summary>
        /// <param name="container">The container.</param>
        public SimpleInjectorHubActivator(Container container)
        {
            _container = container;
        }

        /// <summary>Creates the specified descriptor.</summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns></returns>
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)_container.GetInstance(descriptor.HubType);
        }
    }
}