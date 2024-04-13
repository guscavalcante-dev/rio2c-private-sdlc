// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

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
        public DateTimeOffset? ProducerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? SpeakerTermsAcceptanceDate { get; set; }
        public DateTimeOffset? AvailabilityBeginDate { get; set; }
        public DateTimeOffset? AvailabilityEndDate { get; set; }

        public CollaboratorDto CollaboratorBaseDto { get; set; }
        public AttendeeCollaboratorTypeDto AttendeeCollaboratorTypeDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorBaseDto"/> class.</summary>
        public AttendeeCollaboratorBaseDto()
        {
        }
    }
}