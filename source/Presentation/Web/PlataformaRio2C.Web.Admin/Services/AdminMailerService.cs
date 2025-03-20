// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-19-2023
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
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Web.Admin.Services
{
    /// <summary>AdminMailerService</summary>
    public class AdminMailerService : MailerBase, IMailerService
    {
        private readonly string environment;
        private readonly string siteUrl;
        private readonly string toEmail;
        private readonly string bccEmail;

        /// <summary>Initializes a new instance of the <see cref="AdminMailerService"/> class.</summary>
        public AdminMailerService()
        {
            this.environment = ConfigurationManager.AppSettings["Environment"];
            this.siteUrl = ConfigurationManager.AppSettings["SiteUrl"];
            this.toEmail = ConfigurationManager.AppSettings["MvcMailer.ToEmail"];
            this.bccEmail = ConfigurationManager.AppSettings["MvcMailer.BccEmail"];

            this.MasterName = "_AdminEmailLayout";
        }

        /// <summary>Forgots the password email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage ForgotPasswordEmail(SendForgotPasswordEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(Texts.ForgotPassword, null);
                x.ViewName = "ForgotPasswordEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(false)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(false));
                }
            });
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
                x.Subject = this.GetSubject(string.Format("Bem-vindo ao {0} | Welcome to {0}", cmd.Edition.Name), null);
                //x.Subject = this.GetSubject(string.Format(Labels.WelcomeToEdition, cmd.EditionName));
                x.ViewName = "PlayerWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>Sends the speaker welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendSpeakerWelcomeEmail(SendSpeakerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Bem-vindo ao {0} | Welcome to {0}", cmd.Edition.Name), null);
                //x.Subject = this.GetSubject(string.Format(Labels.WelcomeToEdition, cmd.EditionName));
                x.ViewName = "SpeakerWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>Sends the music commission welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendMusicCommissionWelcomeEmail(SendMusicCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Complete seu cadastro na Comissão de Música do {0} | Complete your registration at {0} Music Commission", cmd.Edition.Name), null);
                x.ViewName = "MusicCommissionWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>Sends the cartoon commission welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <summary>Sends the music commission welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendCartoonCommissionWelcomeEmail(SendCartoonCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Complete seu cadastro na Comissão de Cartoon do {0} | Complete your registration at {0} Cartoon Commission", cmd.Edition.Name), null);
                x.ViewName = "CartoonCommissionWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the innovation commission welcome email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public MvcMailMessage SendInnovationCommissionWelcomeEmail(SendInnovationCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Complete seu cadastro na Comissão de Startup do {0} | Complete your registration at {0} Startup Commission", cmd.Edition.Name), null);
                x.ViewName = "InnovationCommissionWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the Audiovisual commission welcome email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public MvcMailMessage SendAudiovisualCommissionWelcomeEmail(SendAudiovisualCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Complete seu cadastro na Comissão Audiovisual do {0} | Complete your registration at {0} Audiovisual Commission", cmd.Edition.Name), null);
                x.ViewName = "AudiovisualCommissionWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>Sends the admin welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendAdminWelcomeEmail(SendAdminWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Bem-vindo ao {0} | Welcome to {0}", cmd.Edition.Name), null);
                x.ViewName = "AdminWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the players negotiation email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendPlayersNegotiationEmail(SendPlayerNegotiationsEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject("Agenda de Rodadas de Negócio | One-to-One Meetings Agenda", cmd.Edition.Name);
                x.ViewName = "PlayersNegotiationsEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the producers negotiation email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProducersNegotiationEmail(SendProducerNegotiationsEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject("Agenda de Rodadas de Negócio | One-to-One Meetings Agenda", cmd.Edition.Name);
                x.ViewName = "ProducersNegotiationsEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }
        /// <summary>
        /// Sends the producers negotiation email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProducersMusicBusinessRoundEmail(SendMusicBusinessRoundProducerEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject("Agenda de Rodadas de Negócio | One-to-One Meetings Agenda", cmd.Edition.Name);
                x.ViewName = "ProducersNegotiationsEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the creator commission welcome email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendCreatorCommissionWelcomeEmail(SendCreatorCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Complete seu cadastro na Comissão de Creator do {0} | Complete your registration at {0} Creator Commission", cmd.Edition.Name), null);
                x.ViewName = "CreatorCommissionWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>
        /// Sends the executive agenda email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendExecutiveAgendaEmail(SendExecutiveAgendaEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Sua agenda no {0} | Your agenda at {0}", cmd.Edition.Name), null);
                x.ViewName = "ExecutiveAgendaEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;
                ViewBag.SiteUrl = this.siteUrl;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        #region Private methods

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <returns></returns>
        private string GetSubject(string subject, string editionName)
        {
            var emailPrefix = (!string.IsNullOrEmpty(this.environment) && this.environment.ToLower() != "prod" ? "[" + this.environment.ToUpper() + "] " : string.Empty) 
                                    + "MyRio2C | "
                                    + (!string.IsNullOrEmpty(editionName) ? editionName + " | " : string.Empty);

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
        /// <param name="sendProdBccEmail">if set to <c>true</c> [send product BCC email].</param>
        /// <returns></returns>
        private string GetBccEmailRecipient(bool sendProdBccEmail)
        {
            if (!sendProdBccEmail && this.environment.ToLower() == "prod")
            {
                return null;
            }

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

        /// <summary>Sends the producer welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sends the unread conversation email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendUnreadConversationEmail(SendUnreadConversationEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sends the project buyer evaluation email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProjectBuyerEvaluationEmail(SendProjectBuyerEvaluationEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the speakers report email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public MvcMailMessage SendSpeakersReportEmail(SendSpeakersReportEmailAsync cmd)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}