// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-06-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackBaseCommand.cs" company="Softo">
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
        /// <param name="dto">The entity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(AttendeeInnovationOrganizationTrackDto dto)
        {
            this.InnovationOrganizationTrackOptionUid = dto.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = dto.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = dto.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = dto.InnovationOrganizationTrackOption.HasAdditionalInfo;
            this.AdditionalInfo = dto.AttendeeInnovationOrganizationTrack.AdditionalInfo;

            this.InnovationOrganizationTrackOptionGroupUid = dto?.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = dto?.InnovationOrganizationTrackOptionGroup?.Name;

            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="dto">The entity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(AttendeeCollaboratorInnovationOrganizationTrackDto dto)
        {
            this.InnovationOrganizationTrackOptionUid = dto.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = dto.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = dto.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = dto.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.InnovationOrganizationTrackOptionGroupUid = dto?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = dto?.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Name;

            this.AdditionalInfo = "";
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand"/> class.
        /// </summary>
        /// <param name="dto">The entity.</param>
        public InnovationOrganizationTrackOptionBaseCommand(InnovationOrganizationTrackOptionDto dto)
        {
            this.InnovationOrganizationTrackOptionUid = dto.InnovationOrganizationTrackOption.Uid;
            this.InnovationOrganizationTrackOptionName = dto.InnovationOrganizationTrackOption.Name;
            this.InnovationOrganizationTrackOptionDescription = dto.InnovationOrganizationTrackOption.Description;
            this.InnovationOrganizationTrackOptionHasAdditionalInfo = dto.InnovationOrganizationTrackOption.HasAdditionalInfo;

            this.InnovationOrganizationTrackOptionGroupUid = dto.InnovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = dto.InnovationOrganizationTrackOptionGroup?.Name;

            this.IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroup">The innovation organization track option group.</param>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        public InnovationOrganizationTrackOptionBaseCommand(InnovationOrganizationTrackOptionGroup innovationOrganizationTrackOptionGroup, bool isChecked)
        {
            this.InnovationOrganizationTrackOptionGroupUid = innovationOrganizationTrackOptionGroup?.Uid;
            this.InnovationOrganizationTrackOptionGroupName = innovationOrganizationTrackOptionGroup?.Name;

            this.IsChecked = isChecked;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommand" /> class.
        /// </summary>
        public InnovationOrganizationTrackOptionBaseCommand()
        {
        }
    }
}