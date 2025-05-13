// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-28-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInterestBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AttendeeCollaboratorInterestBaseCommand</summary>
    public class AttendeeCollaboratorInterestBaseCommand
    {
        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }
        public bool InterestHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInterestBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public AttendeeCollaboratorInterestBaseCommand(AttendeeCollaboratorInterestDto entity)
        {
            this.InterestUid = entity.InterestUid;
            this.InterestName = entity.InterestName;
            this.InterestHasAdditionalInfo = entity.InterestHasAdditionalInfo;
            this.AdditionalInfo = entity.AttendeeCollaboratorInterestAdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInterestBaseCommand"/> class.
        /// </summary>
        /// <param name="interestDto">The interest dto.</param>
        public AttendeeCollaboratorInterestBaseCommand(InterestDto interestDto)
        {
            this.InterestUid = interestDto.Interest.Uid;
            this.InterestName = interestDto.Interest.Name;
            this.InterestHasAdditionalInfo = interestDto.Interest.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInterestBaseCommand"/> class.</summary>
        public AttendeeCollaboratorInterestBaseCommand()
        {
        }
    }
}