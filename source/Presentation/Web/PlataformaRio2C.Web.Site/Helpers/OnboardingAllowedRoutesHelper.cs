// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="OnboardingAllowedRoutesHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Web.Site.Helpers
{
    /// <summary>
    /// OnboardingAllowedRoutesHelper
    /// </summary>
    public static class OnboardingAllowedRoutesHelper
    {
        /// <summary>
        /// The routes
        /// </summary>
        public static List<OnboardingAllowedRoute> Routes = new List<OnboardingAllowedRoute>
        {
            new OnboardingAllowedRoute("Account", "Login"),
            new OnboardingAllowedRoute("Account", "Logoff"),
            new OnboardingAllowedRoute("Account", "Onboarding"),
            new OnboardingAllowedRoute("Onboarding", "Index"),
            new OnboardingAllowedRoute("Error", "Index"),
            new OnboardingAllowedRoute("Error", "Forbidden"),
            new OnboardingAllowedRoute("Error", "NotFound")
        };

        /// <summary>
        /// Finds the route.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static bool IsRouteAllowed(string controller, string action)
        {
            return Routes.Any(r => r.Controller == controller && r.Action == action);
        }
    }

    /// <summary>
    /// OnboardingAllowedRoute
    /// </summary>
    public class OnboardingAllowedRoute
    {
        public string Controller { get; private set; }
        public string Action { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnboardingAllowedRoute"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        public OnboardingAllowedRoute(string controller, string action)
        {
            this.Controller = controller;
            this.Action = action;
        }
    }
}