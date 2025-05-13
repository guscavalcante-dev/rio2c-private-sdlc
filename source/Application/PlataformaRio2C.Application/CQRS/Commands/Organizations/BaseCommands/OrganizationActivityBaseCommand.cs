// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="OrganizationActivityBaseCommand.cs" company="Softo">
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
    /// <summary>OrganizationActivityBaseCommand</summary>
    public class OrganizationActivityBaseCommand
    {
        public Guid ActivityUid { get; set; }
        public string ActivityName { get; set; }
        public bool ActivityHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivityBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public OrganizationActivityBaseCommand(OrganizationActivityDto entity)
        {
            this.ActivityUid = entity.ActivityUid;
            this.ActivityName = entity.ActivityName;
            this.ActivityHasAdditionalInfo = entity.ActivityHasAdditionalInfo;
            this.AdditionalInfo = entity.OrganizationActivityAdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivityBaseCommand"/> class.</summary>
        /// <param name="activity">The activity.</param>
        public OrganizationActivityBaseCommand(Activity activity)
        {
            this.ActivityUid = activity.Uid;
            this.ActivityName = activity.Name;
            this.ActivityHasAdditionalInfo = activity.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivityBaseCommand"/> class.</summary>
        public OrganizationActivityBaseCommand()
        {
        }
    }
}