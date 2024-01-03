// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Elton Assunção
// Created          : 12-28-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTargetAudiencesDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTargetAudiencesDto</summary>
    public class AttendeeCollaboratorTargetAudiencesDto
    {
        public int AttendeeCollaboratorTargetAudienceId { get; set; }
        public Guid AttendeeCollaboratorTargetAudienceUid { get; set; }
        public string AttendeeCollaboratorTargetAudienceAdditionalInfo { get; set; }

        public int TargetAudienceId { get; set; }
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }
        public bool TargetAudienceHasAdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudiencesDto"/> class.</summary>
        public AttendeeCollaboratorTargetAudiencesDto()
        {
        }


    }
}
