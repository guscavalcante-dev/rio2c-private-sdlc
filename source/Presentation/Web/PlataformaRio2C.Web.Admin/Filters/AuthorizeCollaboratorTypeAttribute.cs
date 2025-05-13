// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-28-2019
// ***********************************************************************
// <copyright file="AuthorizeCollaboratorTypeAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlataformaRio2C.Web.Admin.Filters
{
    /// <summary>AuthorizeCollaboratorTypeAttribute</summary>
    public class AuthorizeCollaboratorTypeAttribute : ActionFilterAttribute
    {
        public string Types { get; set; }

        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isForbidden = false;

            var adminAccessControlDto = (AdminAccessControlDto)filterContext.Controller.ViewBag.AdminAccessControlDto;
            if (adminAccessControlDto != null && adminAccessControlDto.IsAdmin() != true)
            {
                var typesArray = this.Types?.Split(',');
                if (typesArray?.Any() != true)
                {
                    if (adminAccessControlDto.EditionCollaboratorTypes?.Any() != true)
                    {
                        isForbidden = true;
                    }
                }
                else
                {
                    var collaboratorTypes = adminAccessControlDto?.EditionCollaboratorTypes?.Select(eutt => eutt.Name).ToList();
                    if (typesArray.All(allowedType => collaboratorTypes?.Contains(allowedType) != true))
                    {
                        isForbidden = true;
                    }
                }
            }

            if (isForbidden)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Error" },
                        { "action", "Forbidden" },
                        { "area", "" },
                        { "isAjaxRequest", filterContext.HttpContext.Request.IsAjaxRequest() }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}