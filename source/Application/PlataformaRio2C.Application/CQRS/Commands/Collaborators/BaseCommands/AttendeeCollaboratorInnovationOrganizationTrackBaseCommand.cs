// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-28-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInnovationOrganizationTrackBaseCommand.cs" company="Softo">
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
    /// <summary>AttendeeCollaboratorInnovationOrganizationTrackBaseCommand</summary>
    public class AttendeeCollaboratorInnovationOrganizationTrackBaseCommand
    {
        public Guid InnovationOrganizationTrackOptionUid { get; set; }
        public string InnovationOrganizationTrackOptionName { get; set; }
        public bool InnovationOrganizationTrackOptionHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrackBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(AttendeeCollaboratorInnovationOrganizationTrackDto entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = entity.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;
            this.AdditionalInfo = entity.AdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrackBaseCommand"/> class.</summary>
        /// <param name="innovationOrganizationTrackOption">The innovationOrganizationTrack.</param>
        public AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(InnovationOrganizationTrackOption innovationOrganizationTrackOption)
        {
            this.InnovationOrganizationTrackOptionUid = innovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = innovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = innovationOrganizationTrackOption.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrackBaseCommand"/> class.</summary>
        public AttendeeCollaboratorInnovationOrganizationTrackBaseCommand()
        {
        }
    }
}