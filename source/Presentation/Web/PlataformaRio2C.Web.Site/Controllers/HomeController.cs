// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-28-2019
// ***********************************************************************
// <copyright file="DashboardController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>HomeController</summary>
    [Authorize(Order = 1)]
    public class HomeController : Controller
    {
        private readonly IdentityAutenticationService identityController;

        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        public HomeController(IdentityAutenticationService identityController)
        {
            this.identityController = identityController;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            try
            {
                var userId = User.Identity.GetUserId<int>();

                if (await this.identityController.IsInRoleAsync(userId, "Player"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                if (await this.identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "ProducerArea" });
                }

                return RedirectToAction("LogOff", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("LogOff", "Account");
            }
        }
    }
}