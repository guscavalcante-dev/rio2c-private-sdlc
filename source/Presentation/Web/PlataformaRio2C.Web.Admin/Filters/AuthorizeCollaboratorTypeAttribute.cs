// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="AuthorizeCollaboratorTypeAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Web.Admin.Filters
{
    /// <summary>AuthorizeCollaboratorTypeAttribute</summary>
    public class AuthorizeCollaboratorTypeAttribute : ActionFilterAttribute
    {
        public string[] Types { get; set; }

        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.Types?.Any() == true)
            {
                var adminAccessControlDto = filterContext.Controller.ViewBag.AdminAccessControlDto as AdminAccessControlDto;
                if (adminAccessControlDto != null && adminAccessControlDto.IsAdmin() != true)
                {
                    var collaboratorTypes = adminAccessControlDto?.EditionCollaboratorTypes?.Select(eutt => eutt.Name).ToList();
                    if (this.Types.All(allowedCollaboratorType => collaboratorTypes?.Contains(allowedCollaboratorType) != true))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary {
                                { "controller", "Error" },
                                { "action", "Forbidden" },
                                { "area", "" },
                                { "isAjaxRequest", filterContext.HttpContext.Request.IsAjaxRequest() }
                            });
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}