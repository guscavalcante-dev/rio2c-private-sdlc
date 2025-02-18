// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 02-18-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-18-2025
// ***********************************************************************
// <copyright file="AppBuilderExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin;
using Owin;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site
{
    /// <summary>
    /// AppBuilderExtensions
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        public static void ConfigureAuth(this IAppBuilder appBuilder)
        {
            IdentityProviderSetup.DataProtectionProvider = appBuilder.GetDataProtectionProvider();

            // Configure the db context, user manager and signin manager to use a single instance per request
            appBuilder.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager<ApplicationUser>>());

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieName = ".MyRio2C.Site.ApplicationCookie",
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a 
                    // password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator
                    // ADD AN INT AS A THIRD TYPE ARGUMENT:
                    .OnValidateIdentity<ApplicationUserManager<ApplicationUser>, ApplicationUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        // THE NAMED ARGUMENT IS DIFFERENT:
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        // Need to add THIS line because we added the third type argument (int) above:
                        getUserIdCallback: (claim) => int.Parse(claim.GetUserId())
                    )
                }
            });

            appBuilder.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            appBuilder.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            appBuilder.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }

        /// <summary>
        /// Configures the projects to audiovisual business round projects redirect.
        /// This method was created to avoid the error 
        /// "System.InvalidOperationException: Multiple types were found that match the controller named 'projects'"
        /// </summary>
        /// <param name="appBuilder">The application.</param>
        public static void ConfigureProjectsToAudiovisualBusinessRoundProjectsRedirect(this IAppBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                var requestPath = context.Request.Path.Value;
                if (requestPath.EndsWith("/projects", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect("/audiovisual/BusinessRoundProjects");
                    return;
                }

                await next();
            });
        }
    }
}
