// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 08-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="AjaxAuthorizeAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Modules
{
    /// <summary>SuppressFormsAuthenticationRedirectModule</summary>
    public class SuppressFormsAuthenticationRedirectModule : IHttpModule
    {
        private static readonly object SuppressAuthenticationKey = new Object();

        /// <summary>Suppresses the authentication redirect.</summary>
        /// <param name="context">The context.</param>
        public static void SuppressAuthenticationRedirect(HttpContext context)
        {
            context.Items[SuppressAuthenticationKey] = true;
        }

        /// <summary>Suppresses the authentication redirect.</summary>
        /// <param name="context">The context.</param>
        public static void SuppressAuthenticationRedirect(HttpContextBase context)
        {
            context.Items[SuppressAuthenticationKey] = true;
        }

        /// <summary>Initializes a module and prepares it to handle requests.</summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application
        /// </param>
        public void Init(HttpApplication context)
        {
            context.PostReleaseRequestState += OnPostReleaseRequestState;
            context.EndRequest += OnEndRequest;
        }

        /// <summary>Called when [post release request state].</summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnPostReleaseRequestState(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;
            var request = context.Request;

            if (request.FilePath == "/api/v1.0/auth/login" || (response.StatusCode == 401 && request.Headers["X-Requested-With"] == "XMLHttpRequest"))
            {
                SuppressAuthenticationRedirect(context.Context);
            }
        }

        /// <summary>Called when [end request].</summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnEndRequest(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;

            if (context.Context.Items.Contains(SuppressAuthenticationKey))
            {
                response.TrySkipIisCustomErrors = true;
                response.ClearContent();
                response.StatusCode = 401;
                response.RedirectLocation = null;
            }
        }

        /// <summary>Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.</summary>
        public void Dispose()
        {
        }

        /// <summary>Registers this instance.</summary>
        public static void Register()
        {
            DynamicModuleUtility.RegisterModule(
                typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }
}
