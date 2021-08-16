// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="SiteMailerService.cs" company="Softo">
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

namespace PlataformaRio2C.Web.Site.Services
{
    /// <summary>SiteMailerService</summary>
    public class SiteMailerService : MailerBase, IMailerService
    {
        private readonly string environment;
        private readonly string toEmail;
        private readonly string bccEmail;

        /// <summary>Initializes a new instance of the <see cref="SiteMailerService"/> class.</summary>
        public SiteMailerService()
        {
            this.environment = ConfigurationManager.AppSettings["Environment"];
            this.toEmail = ConfigurationManager.AppSettings["MvcMailer.ToEmail"];
            this.bccEmail = ConfigurationManager.AppSettings["MvcMailer.BccEmail"];

            this.MasterName = "_SiteEmailLayout";
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

        /// <summary>Sends the producer welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format("Bem-vindo ao {0} | Welcome to {0}", cmd.Edition.Name), null);
                x.ViewName = "TicketBuyerWelcomeEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(true)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(true));
                }
            });
        }

        /// <summary>Sends the unread conversation email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendUnreadConversationEmail(SendUnreadConversationEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            var senderName = cmd.NotificationEmailConversationDto.OtherAttendeeCollaboratorDto?.Collaborator?.GetDisplayName() ??
                             cmd.NotificationEmailConversationDto.OtherUser?.Name;

            return Populate(x =>
            {
                x.Subject = this.GetSubject(string.Format(Messages.UserSentYouMessage, senderName), cmd.Edition?.Name);
                x.ViewName = "UnreadConversationEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;

                if (!string.IsNullOrEmpty(this.GetBccEmailRecipient(false)))
                {
                    x.Bcc.Add(this.GetBccEmailRecipient(false));
                }
            });
        }

        /// <summary>Sends the project buyer evaluation email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public MvcMailMessage SendProjectBuyerEvaluationEmail(SendProjectBuyerEvaluationEmailAsync cmd, Guid sentEmailUid)
        {
            this.SetCulture(cmd.UserInterfaceLanguage);

            this.ViewData = new ViewDataDictionary(cmd);

            return Populate(x =>
            {
                x.Subject = this.GetSubject(Messages.ProjectBuyerEvaluationEmailSubject, cmd.Edition?.Name);
                x.ViewName = "ProjectBuyerEvaluationEmail";
                x.From = new MailAddress(address: x.From.Address, displayName: "MyRio2C");
                x.To.Add(this.GetToEmailRecipient(cmd.RecipientEmail));
                ViewBag.SentEmailUid = sentEmailUid;

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

        #region Admin Mailers Not implemented

        /// <summary>Sends the player welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendPlayerWelcomeEmail(SendPlayerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sends the speaker welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendSpeakerWelcomeEmail(SendSpeakerWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sends the music commission welcome email.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendMusicCommissionWelcomeEmail(SendMusicCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the admin welcome email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendAdminWelcomeEmail(SendAdminWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the players negotiation email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendPlayersNegotiationEmail(SendPlayerNegotiationsEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the producers negotiation email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendProducersNegotiationEmail(SendProducerNegotiationsEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the audiovisual commission welcome email.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MvcMailMessage SendAudiovisualCommissionWelcomeEmail(SendAudiovisualCommissionWelcomeEmailAsync cmd, Guid sentEmailUid)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}