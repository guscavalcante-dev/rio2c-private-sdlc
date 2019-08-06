// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.Dtos
{
    /// <summary>SalesPlatformWebhookRequestDto</summary>
    public class SalesPlatformWebhookRequestDto
    {
        public Guid Uid { get; set; }
        public int SalesPlatformId { get; set; }
        public string Payload { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsProcessing { get; set; }
        public int ProcessingCount { get; set; }
        public DateTime? LastProcessingDate { get; set; }
        public string SecurityStamp { get; set; }

        public SalesPlatformDto SalesPlatformDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestDto"/> class.</summary>
        public SalesPlatformWebhookRequestDto()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestDto"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public SalesPlatformWebhookRequestDto(Domain.Entities.SalesPlatformWebhookRequest entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Uid = entity.Uid;
            this.SalesPlatformId = entity.SalesPlatformId;
            this.Payload = entity.Payload;
            this.IsProcessed = entity.IsProcessed;
            this.IsProcessing = entity.IsProcessing;
            this.ProcessingCount = entity.ProcessingCount;
            this.LastProcessingDate = entity.LastProcessingDate;
            this.SecurityStamp = entity.SecurityStamp;
            this.SalesPlatformDto = new SalesPlatformDto(entity?.SalesPlatform);
        }
    }
}
