// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-06-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorBaseDto</summary>
    public class AttendeeCollaboratorBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset? WelcomeEmailSendDate { get; set; }
        public DateTimeOffset? OnboardingStartDate { get; set; }
        public DateTimeOffset? OnboardingFinishDate { get; set; }
        public DateTimeOffset? OnboardingUserDate { get; set; }
        public DateTimeOffset? OnboardingCollaboratorDate { get; set; }
        public DateTimeOffset? AudiovisualPlayerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? InnovationPlayerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? MusicPlayerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? AudiovisualProducerBusinessRoundTermsAcceptanceDate { get; set; }
        public DateTimeOffset? AudiovisualProducerPitchingTermsAcceptanceDate { get; set; }        
        public DateTimeOffset? SpeakerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? AvailabilityBeginDate { get; set; }
        public DateTimeOffset? AvailabilityEndDate { get; set; }

        #region Collaborator

        public Guid CollaboratorUid { get; set; }
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }

        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        public string NameAbbreviation => this.FullName.GetTwoLetterCode();

        #endregion

        public CollaboratorDto CollaboratorBaseDto { get; set; }
        public AttendeeCollaboratorTypeDto AttendeeCollaboratorTypeDto { get; set; }
        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorBaseDto"/> class.</summary>
        public AttendeeCollaboratorBaseDto()
        {
        }
    }
}