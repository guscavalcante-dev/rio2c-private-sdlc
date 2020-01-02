// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceParticipantRoleDto</summary>
    public class ConferenceParticipantRoleDto
    {
        public ConferenceParticipantRole ConferenceParticipantRole { get; set; }
        public IEnumerable<ConferenceParticipantRoleTitleDto> ConferenceParticipantRoleTitleDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleDto"/> class.</summary>
        public ConferenceParticipantRoleDto()
        {
        }
    }
}