// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="CommissionAttendeeCollaboratorInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CommissionAttendeeCollaboratorInterestDto</summary>
    public class CommissionAttendeeCollaboratorInterestDto
    {
        public CommissionAttendeeCollaboratorInterest CommissionAttendeeCollaboratorInterest { get; set; }
        public Interest Interest { get; set; }
        public InterestGroup InterestGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CommissionAttendeeCollaboratorInterestDto"/> class.</summary>
        public CommissionAttendeeCollaboratorInterestDto()
        {
        }
    }
}