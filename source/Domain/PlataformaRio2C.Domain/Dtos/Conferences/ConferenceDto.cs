// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceDto</summary>
    public class ConferenceDto
    {
        public Conference Conference { get; set; }
        public RoomDto RoomDto { get; set; }

        public IEnumerable<ConferenceTitleDto> ConferenceTitleDtos { get; set; }
        public IEnumerable<ConferenceSynopsisDto> ConferenceSynopsisDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDto"/> class.</summary>
        public ConferenceDto()
        {
        }
    }
}