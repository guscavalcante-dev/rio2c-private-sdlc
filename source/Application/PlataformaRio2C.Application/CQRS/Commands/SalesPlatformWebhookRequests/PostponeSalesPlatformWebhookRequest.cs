// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="PostponeSalesPlatformWebhookRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>PostponeSalesPlatformWebhookRequest</summary>
    public class PostponeSalesPlatformWebhookRequest : IRequest<Guid>
    {
        public Guid SalesPlatformWebhookRequestUid { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public string SecurityStamp { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="PostponeSalesPlatformWebhookRequest"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="securityStamp">The security stamp.</param>
        public PostponeSalesPlatformWebhookRequest(Guid salesPlatformWebhookRequestUid, string errorCode, string errorMessage, string securityStamp)
        {
            this.SalesPlatformWebhookRequestUid = salesPlatformWebhookRequestUid;
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.SecurityStamp = securityStamp;
        }
    }
}