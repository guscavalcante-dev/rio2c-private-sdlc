// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
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
        #region Admin Emails

        MvcMailMessage SendPlayerWelcomeEmail(SendPlayerWelcomeEmailAsync cmd, Guid sentEmailUid);

        #endregion

        #region Site Emails

        MvcMailMessage SendProducerWelcomeEmail(SendProducerWelcomeEmailAsync cmd, Guid sentEmailUid);
        #endregion
    }
}