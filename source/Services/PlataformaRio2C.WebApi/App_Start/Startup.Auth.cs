﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System;
using System.Web.Mvc;

namespace PlataformaRio2C.WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager<ApplicationUser>>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
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
                regenerateIdentityCallback: (manager, user)
                    => user.GenerateUserIdentityAsync(manager),
                    // Need to add THIS line because we added the third type argument (int) above:
                    getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}
