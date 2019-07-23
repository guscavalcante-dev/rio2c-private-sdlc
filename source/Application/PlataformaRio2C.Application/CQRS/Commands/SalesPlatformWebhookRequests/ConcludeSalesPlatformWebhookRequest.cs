// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="ConcludeSalesPlatformWebhookRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ConcludeSalesPlatformWebhookRequest</summary>
    public class ConcludeSalesPlatformWebhookRequest : IRequest<Guid>
    {
        public Guid SalesPlatformWebhookRequestUid { get; private set; }
        public string SecurityStamp { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConcludeSalesPlatformWebhookRequest"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <param name="securityStamp">The security stamp.</param>
        public ConcludeSalesPlatformWebhookRequest(Guid salesPlatformWebhookRequestUid, string securityStamp)
        {
            this.SalesPlatformWebhookRequestUid = salesPlatformWebhookRequestUid;
            this.SecurityStamp = securityStamp;
        }
    }
}