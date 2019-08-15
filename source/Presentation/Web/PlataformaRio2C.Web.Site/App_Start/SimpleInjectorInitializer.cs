// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PlataformaRio2C.Application.CQRS.CommandsHandlers;
using PlataformaRio2C.Infra.CrossCutting.CQRS;
using PlataformaRio2C.Infra.Data.FileRepository;

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
            //container.Options.PropertySelectionBehavior = new InjectPropertySelectionBehavior();

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

            var activator = new SimpleInjectorHubActivator(container);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);
        }

        /// <summary>Initializes the container.</summary>
        /// <param name="container">The container.</param>
        private static void InitializeContainer(Container container)
        {
            IoCBootStrapper.RegisterServices(container);
            SiteIoCBootStrapper.RegisterServices(container);
            FileRepositoryBootStrapper.RegisterServices(container);
            CqrsBootStrapper.RegisterServices(container, new[]
            {
                typeof(CreateSalesPlatformWebhookRequestCommandHandler).Assembly
            });
        }
    }

    /// <summary>InjectPropertySelectionBehavior</summary>
    public class InjectPropertySelectionBehavior : IPropertySelectionBehavior
    {
        /// <summary>Selects the property.</summary>
        /// <param name="type">The type.</param>
        /// <param name="prop">The property.</param>
        /// <returns></returns>
        public bool SelectProperty(Type type, PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(InjectAttribute)).Any();
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


    //public sealed class SimpleInjectorResolver
    //: Microsoft.AspNet.SignalR.IDependencyResolver
    //{
    //    private Container container;
    //    private IServiceProvider provider;
    //    private DefaultDependencyResolver defaultResolver;

    //    public SimpleInjectorResolver(Container container)
    //    {
    //        this.container = container;
    //        this.provider = container;
    //        this.defaultResolver = new DefaultDependencyResolver();
    //    }

    //    [DebuggerStepThrough]
    //    public object GetService(Type serviceType)
    //    {
    //        // Force the creation of hub implementation to go
    //        // through Simple Injector without failing silently.
    //        if (!serviceType.IsAbstract && typeof(IHub).IsAssignableFrom(serviceType))
    //        {
    //            return this.container.GetInstance(serviceType);
    //        }

    //        return this.provider.GetService(serviceType) ??
    //            this.defaultResolver.GetService(serviceType);
    //    }

    //    [DebuggerStepThrough]
    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        return this.container.GetAllInstances(serviceType);
    //    }

    //    public void Register(Type serviceType, IEnumerable<Func<object>> activators)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public void Register(Type serviceType, Func<object> activator)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public void Dispose()
    //    {
    //        this.defaultResolver.Dispose();
    //    }
    //}
}