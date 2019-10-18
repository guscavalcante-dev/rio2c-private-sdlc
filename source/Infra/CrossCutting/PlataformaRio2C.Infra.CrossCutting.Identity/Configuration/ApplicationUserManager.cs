// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="ApplicationUserManager.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Configuration
{
    /// <summary>ApplicationUserManager</summary>
    /// <typeparam name="U"></typeparam>
    public class ApplicationUserManager<U> : UserManager<U, int>
        where U : User
    {
        /// <summary>Initializes a new instance of the <see cref="ApplicationUserManager{U}"/> class.</summary>
        /// <param name="store">The store.</param>
        /// <param name="setup">The setup.</param>
        public ApplicationUserManager(IUserStore<U, int> store, IdentityServicesSetup setup)
            : base(store)
        {
            // Configurando validator para nome de usuario
            this.UserValidator = new UserValidator<U, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configuração de Lockout
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            ////Providers de Two Factor Autentication
            //RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<U, int>
            //{
            //    MessageFormat = setup.SmsSetup.TwoFactorProviderBodyFormat // "Seu código de segurança é: {0}"
            //});

            //RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<U, int>
            //{
            //    Subject = setup.EmailSetup.TwoFactorProviderSubject, // "Código de Segurança",
            //    BodyFormat = setup.EmailSetup.TwoFactorProviderMessageFormat // "Seu código de segurança é: {0}"
            //});

            //// Definindo a classe de serviço de e-mail
            //EmailService = new IdentityEmailMessageService(setup.EmailSetup);

            //// Definindo a classe de serviço de SMS
            //SmsService = new IdentitySmsMessageService(setup.SmsSetup);

            //var provider = new DpapiDataProtectionProvider("TokenMyRio2Provider");
            //var dataProtector = provider.Create("ASP.NET Identity");
            //this.UserTokenProvider = new DataProtectorTokenProvider<U, int>(dataProtector);

            if (IdentityProviderSetup.DataProtectionProvider != null)
            {
                var provider = IdentityProviderSetup.DataProtectionProvider;
                var dataProtector = provider.Create("TokenMyRio2CProtector");
                this.UserTokenProvider = new DataProtectorTokenProvider<U, int>(dataProtector);
            }
        }
    }

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<PlataformaRio2CContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };

    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };

    //        // Configure user lockout defaults
    //        manager.UserLockoutEnabledByDefault = true;
    //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        // You can write your own provider and plug it in here.
    //        manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
    //        {
    //            MessageFormat = "Your security code is {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
    //        {
    //            Subject = "Security Code",
    //            BodyFormat = "Your security code is {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider =
    //                new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}
}
