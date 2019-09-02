// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SiteMailerService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Configuration;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;
using Mvc.Mailer;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Web.Site.Services
{
    /// <summary>SiteMailerService</summary>
    public class SiteMailerService : MailerBase, ISiteMailerService
    {
        private readonly string environment;
        private readonly string toEmail;
        private readonly string bccEmail;

        /// <summary>Initializes a new instance of the <see cref="SiteMailerService"/> class.</summary>
        public SiteMailerService()
        {
            environment = ConfigurationManager.AppSettings["MvcMailer.Environment"];
            toEmail = ConfigurationManager.AppSettings["MvcMailer.ToEmail"];
            bccEmail = ConfigurationManager.AppSettings["MvcMailer.BccEmail"];

            this.MasterName = "_SiteEmailLayout";
        }

        /// <summary>Sends the welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        public MvcMailMessage SendWelcomeEmail(SendWelmcomeEmailAsync cmd)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format(Labels.WelcomeToEdition, cmd.EditionName));
                x.ViewName = "Welcome";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.Email));

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient()))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient());
                }
            });
        }

        #region Private methods

        /// <summary>Gets the subject.</summary>
        /// <param name="subject">The subject.</param>
        /// <returns></returns>
        private string GetSubject(string subject)
        {
            var emailPrefix = (!string.IsNullOrEmpty(this.environment) && this.environment.ToLower() != "prod" ? "[" + this.environment.ToUpper() + "] " : string.Empty) + "MyRio2C | ";

            return emailPrefix + subject;
        }

        /// <summary>Gets to email recipient.</summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        private string GetToEmailRecipient(string email)
        {
            return environment.ToLower() != "prod" ? this.toEmail : 
                                                     email;
        }

        /// <summary>Gets the BCC email recipient.</summary>
        /// <returns></returns>
        private string GetBccEmailRecipient()
        {
            return this.bccEmail;
        }

        /// <summary>Sets the culture.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void SetCulture(string userInterfaceLanguage)
        {
            // Modify current thread's cultures            
            if (string.IsNullOrEmpty(userInterfaceLanguage))
            {
                return;
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(userInterfaceLanguage);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        #endregion
    }
}