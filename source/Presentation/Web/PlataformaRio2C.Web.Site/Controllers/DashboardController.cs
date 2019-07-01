// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="DashboardController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>DashboardController</summary>
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player")]
    public class DashboardController : BaseController
    {
        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region Breadcrumb

            // Breadcumb
            ViewBag.Breadcrumb = new BreadcrumbHelper("Dashboard", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Dashboard", Url.Action("Index", "Dashboard", new { Area = "" })),
                new BreadcrumbItemHelper("Projects", Url.Action("Index", "Dashboard", new { Area = "" }))
            });

            #endregion

            return View();
        }
    }
}