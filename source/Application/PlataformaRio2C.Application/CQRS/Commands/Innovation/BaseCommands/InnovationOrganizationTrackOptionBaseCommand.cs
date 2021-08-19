// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-18-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackBaseCommand.cs" company="Softo">
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
    /// <summary>InnovationOrganizationTrackBaseCommand</summary>
    public class InnovationOrganizationTrackOptionBaseCommand
    {
        public Guid InnovationOrganizationTrackOptionUid { get; set; }
        public string InnovationOrganizationTrackOptionName { get; set; }
        public string InnovationOrganizationTrackOptionDescription { get; set; }
        public bool InnovationOrganizationTrackOptionHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(AttendeeInnovationOrganizationTrackDto entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = entity.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = entity.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.AdditionalInfo = entity.AttendeeInnovationOrganizationTrack.AdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(AttendeeCollaboratorInnovationOrganizationTrackDto entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = entity.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = entity.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.AdditionalInfo = "";
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOption">The activity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(InnovationOrganizationTrackOption innovationOrganizationTrackOption)
        {
            this.InnovationOrganizationTrackOptionUid = innovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = innovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = innovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = innovationOrganizationTrackOption.HasAdditionalInfo;

            this.IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        public InnovationOrganizationTrackOptionBaseCommand()
        {
        }
    }
}