// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorInterestDto</summary>
    public class AttendeeCollaboratorInterestDto
    {
        public int AttendeeCollaboratorInterestId { get; set; }
        public Guid AttendeeCollaboratorInterestUid { get; set; }
        public string AttendeeCollaboratorInterestAdditionalInfo { get; set; }

        public int InterestId { get; set; }
        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }
        public bool InterestHasAdditionalInfo { get; set; }

        public AttendeeCollaboratorInterest AttendeeCollaboratorInterest { get; set; }
        public Interest Interest { get; set; }
        public InterestGroup InterestGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInterestDto"/> class.</summary>
        public AttendeeCollaboratorInterestDto()
        {
        }
    }
}