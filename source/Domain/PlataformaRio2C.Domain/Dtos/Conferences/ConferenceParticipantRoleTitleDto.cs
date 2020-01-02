// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleTitleDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceParticipantRoleTitleDto</summary>
    public class ConferenceParticipantRoleTitleDto
    {
        public ConferenceParticipantRoleTitle ConferenceParticipantRoleTitle { get; set; }
        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitleDto"/> class.</summary>
        public ConferenceParticipantRoleTitleDto()
        {
        }
    }
}