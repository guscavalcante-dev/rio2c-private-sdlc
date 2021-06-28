// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="IMailerService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Mvc.Mailer;
using PlataformaRio2C.Application.CQRS.Commands;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>IMailerService</summary>
    public interface IMailerService
    {
        #region Common Emails

        MvcMailMessage ForgotPasswordEmail(SendForgotPasswordEmailAsync cmd, Guid sentEmailUid);

        #endregion

        #region Admin Emails

        MvcMailMessage SendPlayerWelcomeEmail(SendPlayerWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendSpeakerWelcomeEmail(SendSpeakerWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendMusicCommissionWelcomeEmail(SendMusicCommissionWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendAdminWelcomeEmail(SendAdminWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendPlayersNegotiationEmail(SendPlayerNegotiationsEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendProducersNegotiationEmail(SendProducerNegotiationsEmailAsync cmd, Guid sentEmailUid);

        #endregion

        #region Site Emails

        MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendUnreadConversationEmail(SendUnreadConversationEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendProjectBuyerEvaluationEmail(SendProjectBuyerEvaluationEmailAsync cmd, Guid sentEmailUid);

        #endregion
    }
}