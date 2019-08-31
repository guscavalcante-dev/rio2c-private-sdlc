// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="ProcessPendingPlatformWebhookRequestsAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProcessPendingPlatformWebhookRequestsAsync</summary>
    public class ProcessPendingPlatformWebhookRequestsAsync : IRequest<AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="ProcessPendingPlatformWebhookRequestsAsync"/> class.</summary>
        public ProcessPendingPlatformWebhookRequestsAsync()
        {
        }
    }
}