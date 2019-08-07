// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Web.Site.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Site.Areas.Player.Controllers
{
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1/*, Roles = "Player"*/)]
    //[AuthorizeTicketType(new[] { "Player" })]
    public class HomeController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        // GET: ProducerArea/Dashboard
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Área do Player", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Dashboard", Url.Action("Index", "Home", new { Area = "Player" }))
            });

            #endregion

            return View();
        }
    }
}