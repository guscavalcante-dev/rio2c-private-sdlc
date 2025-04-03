// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-03-2024
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
        MvcMailMessage SendCartoonCommissionWelcomeEmail(SendCartoonCommissionWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendInnovationCommissionWelcomeEmail(SendInnovationCommissionWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendAudiovisualCommissionWelcomeEmail(SendAudiovisualCommissionWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendAdminWelcomeEmail(SendAdminWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendPlayersNegotiationEmail(SendPlayerNegotiationsEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendProducersNegotiationEmail(SendProducerNegotiationsEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendProducersMusicBusinessRoundEmail(SendMusicBusinessRoundProducerEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendCreatorCommissionWelcomeEmail(SendCreatorCommissionWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendExecutiveAgendaEmail(SendExecutiveAgendaEmailAsync cmd, Guid sentEmailUid);

        #endregion

        #region Site Emails

        MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendUnreadConversationEmail(SendUnreadConversationEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendProjectBuyerEvaluationEmail(SendProjectBuyerEvaluationEmailAsync cmd, Guid sentEmailUid);
        MvcMailMessage SendSpeakersReportEmail(SendSpeakersReportEmailAsync cmd);

        #endregion
    }
}