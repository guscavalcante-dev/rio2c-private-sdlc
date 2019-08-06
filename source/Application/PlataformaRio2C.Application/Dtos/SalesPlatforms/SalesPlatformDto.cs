// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="SalesPlatformDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.Dtos
{
    /// <summary>SalesPlatformDto</summary>
    public class SalesPlatformDto
    {
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public Guid WebhookSecurityKey { get; private set; }
        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
        public int MaxProcessingCount { get; private set; }
        public int CreationUserId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int UpdateUserId { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public string SecurityStamp { get; private set; }

        //public UserAppViewModel Creator { get; set; }
        //public UserAppViewModel Updated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformDto"/> class.</summary>
        public SalesPlatformDto()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformDto"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public SalesPlatformDto(Domain.Entities.SalesPlatform entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Uid = entity.Uid;
            this.Name = entity.Name;
            this.IsActive = entity.IsActive;
            this.WebhookSecurityKey = entity.WebhookSecurityKey;
            this.ApiKey = entity.ApiKey;
            this.ApiSecret = entity.ApiSecret;
            this.MaxProcessingCount = entity.MaxProcessingCount;
            this.CreationUserId = entity.CreationUserId;
            this.CreationDate = entity.CreateDate;
            this.UpdateUserId = entity.UpdateUserId;
            this.UpdateDate = entity.UpdateDate;
            this.SecurityStamp = entity.SecurityStamp;

            //if (entity.Creator != null)
            //{
            //    this.Creator = new UserAppViewModel(entity.Creator);
            //}

            //if (entity.Updater != null)
            //{
            //    this.Updater = new UserAppViewModel(entity.Updater);
            //}
        }
    }
}
