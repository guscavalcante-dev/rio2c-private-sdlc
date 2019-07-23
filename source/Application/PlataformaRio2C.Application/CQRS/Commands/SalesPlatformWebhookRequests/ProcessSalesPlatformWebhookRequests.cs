// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="ProcessSalesPlatformWebhookRequests.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProcessSalesPlatformWebhookRequests</summary>
    public class ProcessSalesPlatformWebhookRequests : IRequest<List<Guid>>
    {
        public List<Guid> SalesPlatformWebhookRequestsUids { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProcessSalesPlatformWebhookRequests"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestsUids">The sales platform webhook requests uids.</param>
        public ProcessSalesPlatformWebhookRequests(List<Guid> salesPlatformWebhookRequestsUids)
        {
            this.SalesPlatformWebhookRequestsUids = salesPlatformWebhookRequestsUids;
        }
    }
}