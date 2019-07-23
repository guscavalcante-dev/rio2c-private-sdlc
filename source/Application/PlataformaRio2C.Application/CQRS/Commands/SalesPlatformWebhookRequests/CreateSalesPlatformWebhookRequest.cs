// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="CreateSalesPlatformWebhookRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateSalesPlatformWebhookRequest</summary>
    public class CreateSalesPlatformWebhookRequest : IRequest<Guid?>
    {
        public Guid SalesPlatformWebhookRequestUid { get; private set; }
        public string SalesPlatformName { get; private set; }
        public string WebhookSecurityKey { get; private set; }
        public string Endpoint { get; private set; }
        public string Header { get; private set; }
        public string Payload { get; private set; }
        public string IpAddress { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateSalesPlatformWebhookRequest"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <param name="salesPlatformName">Name of the sales platform.</param>
        /// <param name="webhooSecurityKey">The webhoo security key.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="header">The header.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="ipAddress">The ip address.</param>
        public CreateSalesPlatformWebhookRequest(
            Guid salesPlatformWebhookRequestUid,
            string salesPlatformName,
            string webhooSecurityKey,
            string endpoint,
            string header,
            string payload,
            string ipAddress)
        {
            this.SalesPlatformWebhookRequestUid = salesPlatformWebhookRequestUid;
            this.SalesPlatformName = salesPlatformName;
            this.WebhookSecurityKey = webhooSecurityKey;
            this.Endpoint = endpoint;
            this.Header = header;
            this.Payload = payload;
            this.IpAddress = ipAddress;
        }
    }
}