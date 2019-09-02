// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="ISiteMailerService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Mvc.Mailer;
using PlataformaRio2C.Application.CQRS.Commands;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>ISiteMailerService</summary>
    public interface ISiteMailerService
    {
        MvcMailMessage SendWelcomeEmail(SendWelmcomeEmailAsync cmd);
    }
}