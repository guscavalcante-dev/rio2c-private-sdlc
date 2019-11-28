// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="SimpleInjectorHubActivator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.SignalR.Hubs;
using SimpleInjector;

namespace PlataformaRio2C.Web.Site.Hub
{
    /// <summary>SimpleInjectorHubActivator</summary>
    public class SimpleInjectorHubActivator : IHubActivator
    {
        private readonly Container container;

        /// <summary>Initializes a new instance of the <see cref="SimpleInjectorHubActivator"/> class.</summary>
        /// <param name="container">The container.</param>
        public SimpleInjectorHubActivator(Container container)
        {
            this.container = container;
        }

        /// <summary>Creates the specified descriptor.</summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns></returns>
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)this.container.GetInstance(descriptor.HubType);
        }
    }
}