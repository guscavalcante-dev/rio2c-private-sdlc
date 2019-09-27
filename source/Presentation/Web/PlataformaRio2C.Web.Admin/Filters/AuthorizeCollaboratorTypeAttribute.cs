// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
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
        public string[] AllowedCollaboratorTypes { get; set; }

        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.AllowedCollaboratorTypes?.Any() == true)
            {
                var hasType = false;

                var userAccessControlDto = (UserAccessControlDto)filterContext.Controller.ViewBag.UserAccessControlDto;
                if (userAccessControlDto != null && userAccessControlDto.Roles?.All(r => r.Name != "Admin") == true)
                {
                    var collaboratorTypes = userAccessControlDto?.EditionCollaboratorTypes?.Select(eutt => eutt.Name).ToList();
                    foreach (var allowedCollaboratorType in this.AllowedCollaboratorTypes)
                    {
                        if (collaboratorTypes?.Contains(allowedCollaboratorType) == true)
                        {
                            hasType = true;
                            break;
                        }
                    }

                    if (!hasType)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Forbidden", area = "" }));
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}