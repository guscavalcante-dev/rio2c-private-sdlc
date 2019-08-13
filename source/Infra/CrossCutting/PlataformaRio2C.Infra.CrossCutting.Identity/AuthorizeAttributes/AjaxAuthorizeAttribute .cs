// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 08-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-13-2019
// ***********************************************************************
// <copyright file="AjaxAuthorizeAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes
{
    /// <summary>AjaxAuthorizeAttribute</summary>
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>Handles the unauthorized request.</summary>
        /// <param name="context">The context.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new JsonResult
                    {
                        Data = new
                        {
                            status = "error",
                            error = "Unauthorized"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    var urlHelper = new UrlHelper(context.RequestContext);
                    context.Result = new JsonResult
                    {
                        Data = new
                        {
                            status = "error",
                            error = "Forbidden",
                            redirect = urlHelper.Action("Forbidden", "Error", new { Area = "" })
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}