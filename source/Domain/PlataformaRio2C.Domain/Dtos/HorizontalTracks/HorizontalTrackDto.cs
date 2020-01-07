// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="HorizontalTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>HorizontalTrackDto</summary>
    public class HorizontalTrackDto
    {
        public HorizontalTrack HorizontalTrack { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackDto"/> class.</summary>
        public HorizontalTrackDto()
        {
        }

        /// <summary>Gets the name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameByLanguageCode(string languageCode)
        {
            return this.HorizontalTrack?.Name.GetSeparatorTranslation(languageCode, '|');
        }
    }
}