// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="AuthorizeTicketTypeFilterAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlataformaRio2C.Web.Site.Filters
{
    /// <summary>AuthorizeTicketType</summary>
    public class AuthorizeTicketType : ActionFilterAttribute
    {
        private string[] allowedTicketTYpes;

        /// <summary>Initializes a new instance of the <see cref="AuthorizeTicketType"/> class.</summary>
        /// <param name="allowedTicketTypes">The allowed ticket types.</param>
        public AuthorizeTicketType(string[] allowedTicketTypes)
        {
            this.allowedTicketTYpes = allowedTicketTypes;
        }

        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.allowedTicketTYpes?.Any() == true)
            {
                var hasTicket = false;

                var userTicketTypes = (string[])filterContext.Controller.ViewBag.UserTicketTypes;
                foreach (var allowedTicketTYpe in this.allowedTicketTYpes)
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

            base.OnActionExecuting(filterContext);
        }
    }
}