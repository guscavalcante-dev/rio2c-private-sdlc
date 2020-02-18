// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="SalesPlatformDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

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

        //public UserAppViewModel Creator { get; set; }
        //public UserAppViewModel Updated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformDto"/> class.</summary>
        public SalesPlatformDto()
        {
        }
    }
}
