// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="SalesPlatformBaseViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>SalesPlatformBaseViewModel</summary>
    public class SalesPlatformBaseViewModel
    {
        public Guid Uid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Labels))]
        public bool IsActive { get; set; }

        [Display(Name = "WebhookSecurityKey", ResourceType = typeof(Labels))]
        public Guid WebhookSecurityKey { get; set; }

        [Display(Name = "ApiKey", ResourceType = typeof(Labels))]
        public string ApiKey { get; set; }

        [Display(Name = "ApiSecret", ResourceType = typeof(Labels))]
        public string ApiSecret { get; set; }

        [Display(Name = "MaxProcessingCount", ResourceType = typeof(Labels))]
        public int MaxProcessingCount { get; set; }

        [Display(Name = "CreationUserId", ResourceType = typeof(Labels))]
        public int CreationUserId { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Labels))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "UpdateUserId", ResourceType = typeof(Labels))]
        public int UpdateUserId { get; set; }

        [Display(Name = "UpdateDate", ResourceType = typeof(Labels))]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "SecurityStamp", ResourceType = typeof(Labels))]
        public string SecurityStamp { get; set; }

        //public UserAppViewModel Creator { get; set; }
        //public UserAppViewModel Updated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformBaseViewModel"/> class.</summary>
        public SalesPlatformBaseViewModel()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformBaseViewModel"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public SalesPlatformBaseViewModel(Domain.Entities.SalesPlatform entity)
        {
            this.Uid = entity.Uid;
            this.Name = entity.Name;
            this.IsActive = entity.IsActive;
            this.WebhookSecurityKey = entity.WebhookSecurityKey;
            this.ApiKey = entity.ApiKey;
            this.ApiSecret = entity.ApiSecret;
            this.MaxProcessingCount = entity.MaxProcessingCount;
            this.CreationUserId = entity.CreationUserId;
            this.CreationDate = entity.CreationDate;
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
