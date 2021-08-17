// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2021
// ***********************************************************************
// <copyright file="ProjectInterestBaseCommand.cs" company="Softo">
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
    /// <summary>ProjectInterestBaseCommand</summary>
    public class ProjectInterestBaseCommand
    {
        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }
        public bool InterestHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ProjectInterestBaseCommand" /> class.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        //public ProjectInterestBaseCommand(AttendeeInnovationOrganizationTrackDto entity)
        //{
        //    this.InterestUid = entity.InnovationOrganizationTrackOption.Uid;
        //    this.InterestName = entity.InnovationOrganizationTrackOption.Name;
        //    this.InterestHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

        //    this.AdditionalInfo = entity.AttendeeInnovationOrganizationTrack.AdditionalInfo;
        //    this.IsChecked = true;
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ProjectInterestBaseCommand" /> class.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        //public ProjectInterestBaseCommand(AttendeeCollaboratorInnovationOrganizationTrackDto entity)
        //{
        //    this.InterestUid = entity.InnovationOrganizationTrackOption.Uid;
        //    this.InterestName = entity.InnovationOrganizationTrackOption.Name;
        //    this.InterestHasAdditionalInfo = entity.InnovationOrganizationTrackOption.HasAdditionalInfo;

        //    this.AdditionalInfo = "";
        //    this.IsChecked = true;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInterestBaseCommand" /> class.
        /// </summary>
        /// <param name="interest">The activity.</param>
        public ProjectInterestBaseCommand(Interest interest)
        {
            this.InterestUid = interest.Uid;
            this.InterestName = interest.Name;
            this.InterestHasAdditionalInfo = interest.HasAdditionalInfo;

            this.IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInterestBaseCommand" /> class.
        /// </summary>
        public ProjectInterestBaseCommand()
        {
        }
    }
}