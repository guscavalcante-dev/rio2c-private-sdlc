// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatformDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SalesPlatformDto</summary>
    public class SalesPlatformDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid WebhookSecurityKey { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public int MaxProcessingCount { get; set; }
        public int CreationUserId { get; set; }
        public DateTimeOffset CreationDate { get;  set; }
        public int UpdateUserId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string SecurityStamp { get; set; }

        public IEnumerable<AttendeeSalesPlatform> AttendeeSalesPlatforms { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformDto"/> class.</summary>
        public SalesPlatformDto()
        {
        }
    }
}
