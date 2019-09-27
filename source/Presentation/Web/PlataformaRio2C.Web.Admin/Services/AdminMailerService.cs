// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="AdminMailerService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;
using Mvc.Mailer;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Services;

namespace PlataformaRio2C.Web.Admin.Services
{
    /// <summary>AdminMailerService</summary>
    public class AdminMailerService : MailerBase, IMailerService
    {
        private readonly string environment;
        private readonly string toEmail;
        private readonly string bccEmail;

        /// <summary>Initializes a new instance of the <see cref="AdminMailerService"/> class.</summary>
        public AdminMailerService()
        {
            environment = ConfigurationManager.AppSettings["Environment"];
            toEmail = ConfigurationManager.AppSettings["MvcMailer.ToEmail"];
            bccEmail = ConfigurationManager.AppSettings["MvcMailer.BccEmail"];

            this.MasterName = "_AdminEmailLayout";
        }

        /// <summary>Sends the player welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendPlayerWelcomeEmail(SendPlayerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Bem-vindo ao {0} | Welcome to {0}", cmd.EditionName));
                //x.Subject = this.GetSubject(string.Format(Labels.WelcomeToEdition, cmd.EditionName));
                x.ViewName = "PlayerWelcome";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;

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

        #region Site Mailers Not implemented

        public MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}