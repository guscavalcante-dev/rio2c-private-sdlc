// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="PresentationFormatDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PresentationFormatDto</summary>
    public class PresentationFormatDto
    {
        public PresentationFormat PresentationFormat { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormatDto"/> class.</summary>
        public PresentationFormatDto()
        {
        }
    }
}