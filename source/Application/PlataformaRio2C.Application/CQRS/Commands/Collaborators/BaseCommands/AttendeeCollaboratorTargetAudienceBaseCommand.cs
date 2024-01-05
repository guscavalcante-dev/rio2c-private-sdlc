// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Elton Assunção
// Created          : 12-28-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTargetAudienceBaseCommand.cs" company="Softo">
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
    public class AttendeeCollaboratorTargetAudienceBaseCommand
    {
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }
        public bool TargetAudienceHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudienceBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public AttendeeCollaboratorTargetAudienceBaseCommand(AttendeeCollaboratorTargetAudiencesDto entity)
        {
            this.TargetAudienceUid = entity.TargetAudienceUid;
            this.TargetAudienceName = entity.TargetAudienceName;
            this.TargetAudienceHasAdditionalInfo = entity.TargetAudienceHasAdditionalInfo;
            this.AdditionalInfo = entity.AttendeeCollaboratorTargetAudienceAdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudienceBaseCommand"/> class.</summary>
        /// <param name="targetAudience">The TargetAudience.</param>
        public AttendeeCollaboratorTargetAudienceBaseCommand(TargetAudience targetAudience)
        {
            this.TargetAudienceUid = targetAudience.Uid;
            this.TargetAudienceName = targetAudience.Name;
            this.TargetAudienceHasAdditionalInfo = targetAudience.HasAdditionalInfo;
            this.IsChecked = false;
        }

        public AttendeeCollaboratorTargetAudienceBaseCommand()
        {
        }
    }
}
