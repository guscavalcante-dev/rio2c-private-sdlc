// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceParticipantRoleJsonDto</summary>
    public class ConferenceParticipantRoleJsonDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleJsonDto"/> class.</summary>
        public ConferenceParticipantRoleJsonDto()
        {
        }
    }
}