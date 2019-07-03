// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Web.Site.Controllers;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class HomeController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(IdentityAutenticationService identityController)
            : base(identityController)
        {
        }
        
        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Dashboard", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Dashboard", Url.Action("Index", "Home", new { Area = "Producer" }))
            });

            #endregion

            return View();
        }
    }
}