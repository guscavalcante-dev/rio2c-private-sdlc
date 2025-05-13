// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SalesPlatformWebhookRequestDto</summary>
    public class SalesPlatformWebhookRequestDto
    {
        public Guid Uid { get; set; }
        public string Payload { get; set; }

        public SalesPlatformWebhookRequest SalesPlatformWebhookRequest { get; set; }
        public SalesPlatformDto SalesPlatformDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestDto"/> class.</summary>
        public SalesPlatformWebhookRequestDto()
        {
        }
    }
}
