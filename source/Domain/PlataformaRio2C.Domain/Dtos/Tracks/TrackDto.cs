// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="TrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>TrackDto</summary>
    public class TrackDto
    {
        public Track Track { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="TrackDto"/> class.</summary>
        public TrackDto()
        {
        }

        /// <summary>Gets the name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameByLanguageCode(string languageCode)
        {
            return this.Track?.Name.GetSeparatorTranslation(languageCode, '|');
        }
    }
}