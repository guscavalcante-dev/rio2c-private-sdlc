// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-28-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorActivityDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorActivityDto</summary>
    public class AttendeeCollaboratorActivityDto
    {
        public int AttendeeCollaboratorActivityId { get; set; }
        public Guid AttendeeCollaboratorActivityUid { get; set; }
        public string AttendeeCollaboratorActivityAdditionalInfo { get; set; }

        public int ActivityId { get; set; }
        public Guid ActivityUid { get; set; }
        public string ActivityName { get; set; }
        public bool ActivityHasAdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorActivityDto"/> class.</summary>
        public AttendeeCollaboratorActivityDto()
        {
        }
    }
}