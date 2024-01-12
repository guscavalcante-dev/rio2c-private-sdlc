// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2024
// ***********************************************************************
// <copyright file="OnboardingAllowedRoutesHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Web.Site.Controllers;
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
            new OnboardingAllowedRoute(nameof(AccountController), nameof(AccountController.Login)),
            new OnboardingAllowedRoute(nameof(AccountController), nameof(AccountController.LogOff)),
            new OnboardingAllowedRoute(nameof(AccountController), nameof(AccountController.Onboarding)),
            new OnboardingAllowedRoute(nameof(ErrorController), nameof(ErrorController.Index)),
            new OnboardingAllowedRoute(nameof(ErrorController), nameof(ErrorController.Forbidden)),
            new OnboardingAllowedRoute(nameof(ErrorController), nameof(ErrorController.NotFound)),
            new OnboardingAllowedRoute(nameof(HomeController), nameof(HomeController.SetCulture)),
            new OnboardingAllowedRoute(nameof(StatesController), nameof(StatesController.FindAllByCountryUid)),
            new OnboardingAllowedRoute(nameof(CitiesController), nameof(CitiesController.FindAllByStateUid)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.Index)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.AudiovisualPlayerTermsAcceptance)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.InnovationPlayerTermsAcceptance)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.MusicPlayerTermsAcceptance)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.AccessData)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.CollaboratorData)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.PlayerInfo)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.PlayerInterests)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.CompanyInfo)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.SkipCompanyInfo)),
            new OnboardingAllowedRoute(nameof(OnboardingController), nameof(OnboardingController.SpeakerTermsAcceptance)),
            new OnboardingAllowedRoute(nameof(CompaniesController), nameof(CompaniesController.ShowTicketBuyerFilledForm)),
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
            this.Controller = controller.Replace("Controller", "");
            this.Action = action;
        }
    }
}