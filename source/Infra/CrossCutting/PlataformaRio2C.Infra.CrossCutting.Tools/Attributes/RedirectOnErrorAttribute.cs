// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 02-14-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-14-2020
// ***********************************************************************
// <copyright file="HandleAntiforgeryTokenException.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using System.Web.Routing;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>
    /// Handle Antiforgery token exception and redirect to customer area if the user is Authenticated
    /// </summary>
    public class HandleAntiforgeryTokenException : HandleErrorAttribute
    {
        /// <summary>
        /// Override the on exception method and check if the user is authenticated and redirect the user 
        /// to the customer service index otherwise continue with the base implamentation
        /// </summary>
        /// <param name="filterContext">Current Exception Context of the request</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpAntiForgeryException && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Set response code back to normal
                filterContext.HttpContext.Response.StatusCode = 200;

                // Handle the exception
                filterContext.ExceptionHandled = true;

                var urlH = new UrlHelper(filterContext.HttpContext.Request.RequestContext);

                // Create a new request context
                var rc = new RequestContext(filterContext.HttpContext, filterContext.RouteData);

                // Create a new return url
                var url = RouteTable.Routes.GetVirtualPath(rc, new RouteValueDictionary(new { Controller = "Home", action = "Index", Area = "" }))?.VirtualPath;

                // Check if there is a request url
                var returnUrl = filterContext.HttpContext.Request.Params["ReturnUrl"];
                if (returnUrl != null && urlH.IsLocalUrl(returnUrl) && !returnUrl.Contains("LogOff"))
                {
                    url = returnUrl;
                }

                // Redirect the user back to the customer service index page
                filterContext.HttpContext.Response.Redirect(url, true);
            }
            else
            {
                // Continue to the base
                base.OnException(filterContext);
            }
        }
    }
}