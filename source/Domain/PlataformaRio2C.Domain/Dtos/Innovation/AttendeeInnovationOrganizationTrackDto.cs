// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationTrackDto</summary>
    public class AttendeeInnovationOrganizationTrackDto
    {
        public int AttendeeInnovationOrganizationTrackId { get; set; }
        public Guid AttendeeInnovationOrganizationTrackUid { get; set; }
        public string AttendeeInnovationOrganizationTrackAdditionalInfo { get; set; }
        
        public int InnovationOrganizationTrackOptionId { get; set; }
        public Guid InnovationOrganizationTrackOptionUid { get; set; }
        public string InnovationOrganizationTrackOptionName { get; set; }
        public bool InnovationOrganizationTrackOptionHasAdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackDto"/> class.</summary>
        public AttendeeInnovationOrganizationTrackDto()
        {
        }
    }
}