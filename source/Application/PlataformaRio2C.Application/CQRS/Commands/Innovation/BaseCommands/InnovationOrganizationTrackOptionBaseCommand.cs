// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-30-2022
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

        public Guid? InnovationOrganizationTrackOptionGroupUid { get; set; }
        public string InnovationOrganizationTrackOptionGroupName { get; set; }

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

            this.InnovationOrganizationTrackOptionGroupUid = entity?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = entity?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Name;

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

            this.InnovationOrganizationTrackOptionGroupUid = entity?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = entity?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Name;

            this.AdditionalInfo = "";
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="entity">The activity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(InnovationOrganizationTrackOption entity)
        {
            this.InnovationOrganizationTrackOptionUid = entity.Uid;
            this.InnovationOrganizationTrackOptionName = entity.Name;
            this.InnovationOrganizationTrackOptionDescription = entity.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = entity.HasAdditionalInfo;

            this.InnovationOrganizationTrackOptionGroupUid = entity?.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = entity?.InnovationOrganizationTrackOptionGroup?.Name;

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