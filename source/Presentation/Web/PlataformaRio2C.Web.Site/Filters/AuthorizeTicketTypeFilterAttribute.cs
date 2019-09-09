// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="AuthorizeTicketTypeFilterAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Web.Site.Filters
{
    /// <summary>AuthorizeTicketType</summary>
    public class AuthorizeTicketType : ActionFilterAttribute
    {
        public string[] AllowedTicketTypes { get; set; }

        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.AllowedTicketTypes?.Any() == true)
            {
                var hasTicket = false;

                var userAccessControlDto = (UserAccessControlDto)filterContext.Controller.ViewBag.UserAccessControlDto;
                if (userAccessControlDto != null && userAccessControlDto.Roles?.All(r => r.Name != "Admin") == true)
                {
                    var userTicketTypes = userAccessControlDto?.EditionUserTicketTypes?.Select(eutt => eutt.Name).ToList();
                    foreach (var allowedTicketTYpe in this.AllowedTicketTypes)
                    {
                        if (userTicketTypes.Contains(allowedTicketTYpe))
                        {
                            hasTicket = true;
                            break;
                        }
                    }

                    if (!hasTicket)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Forbidden", area = "" }));
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}