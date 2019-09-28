// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-28-2019
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
        public DateTime? WelcomeEmailSendDate { get; set; }
        public DateTime? OnboardingStartDate { get; set; }
        public DateTime? OnboardingFinishDate { get; set; }
        public DateTime? OnboardingUserDate { get; set; }
        public DateTime? OnboardingCollaboratorDate { get; set; }
        public DateTime? PlayerTermsAcceptanceDate { get; set; }
        public DateTime? ProducerTermsAcceptanceDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorBaseDto"/> class.</summary>
        public AttendeeCollaboratorBaseDto()
        {
        }
    }
}