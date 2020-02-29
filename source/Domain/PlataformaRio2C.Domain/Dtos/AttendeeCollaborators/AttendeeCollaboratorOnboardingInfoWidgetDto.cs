// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="AttendeeCollaboratorOnboardingInfoWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorOnboardingInfoWidgetDto</summary>
    public class AttendeeCollaboratorOnboardingInfoWidgetDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public User User { get; set; }
        public AttendeeCollaboratorTypeDto AttendeeCollaboratorTypeDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorOnboardingInfoWidgetDto"/> class.</summary>
        public AttendeeCollaboratorOnboardingInfoWidgetDto()
        {
        }
    }
}