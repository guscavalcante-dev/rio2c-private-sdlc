// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-22-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-22-2023
// ***********************************************************************
// <copyright file="OrganizationTargetAudienceBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OrganizationTargetAudienceBaseCommand</summary>
    public class OrganizationTargetAudienceBaseCommand
    {
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }
        public bool TargetAudienceHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public OrganizationTargetAudienceBaseCommand(OrganizationTargetAudienceDto entity)
        {
            this.TargetAudienceUid = entity.TargetAudienceUid;
            this.TargetAudienceName = entity.TargetAudienceName;
            this.TargetAudienceHasAdditionalInfo = entity.TargetAudienceHasAdditionalInfo;
            this.AdditionalInfo = entity.OrganizationTargetAudienceAdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceBaseCommand"/> class.</summary>
        /// <param name="TargetAudience">The TargetAudience.</param>
        public OrganizationTargetAudienceBaseCommand(TargetAudience TargetAudience)
        {
            this.TargetAudienceUid = TargetAudience.Uid;
            this.TargetAudienceName = TargetAudience.Name;
            this.TargetAudienceHasAdditionalInfo = TargetAudience.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceBaseCommand"/> class.</summary>
        public OrganizationTargetAudienceBaseCommand()
        {
        }
    }
}