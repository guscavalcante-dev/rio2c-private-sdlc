// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTrackBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AttendeeInnovationOrganizationTrackBaseCommand</summary>
    public class AttendeeInnovationOrganizationTrackBaseCommand
    {
        public Guid InnovationOrganizationTrackOptionUid { get; set; }
        public string InnovationOrganizationTrackOptionName { get; set; }
        public string InnovationOrganizationTrackOptionDescription { get; set; }
        public bool InnovationOrganizationTrackOptionHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackBaseCommand" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public AttendeeInnovationOrganizationTrackBaseCommand(AttendeeInnovationOrganizationTrackDto entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = entity.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = entity.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.AdditionalInfo = entity.AttendeeInnovationOrganizationTrack.AdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackBaseCommand" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public AttendeeInnovationOrganizationTrackBaseCommand(AttendeeCollaboratorInnovationOrganizationTrackDto entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = entity.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = entity.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.AdditionalInfo = "";
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackBaseCommand" /> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOption">The activity.</param>
        public AttendeeInnovationOrganizationTrackBaseCommand(InnovationOrganizationTrackOption innovationOrganizationTrackOption)
        {
            this.InnovationOrganizationTrackOptionUid = innovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = innovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = innovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = innovationOrganizationTrackOption.HasAdditionalInfo;

            this.IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackBaseCommand" /> class.
        /// </summary>
        public AttendeeInnovationOrganizationTrackBaseCommand()
        {
        }
    }
}