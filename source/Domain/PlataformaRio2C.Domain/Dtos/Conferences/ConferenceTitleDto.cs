// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceTitleDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceTitleDto</summary>
    public class ConferenceTitleDto
    {
        public ConferenceTitle ConferenceTitle { get; set; }
        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTitleDto"/> class.</summary>
        public ConferenceTitleDto()
        {
        }
    }
}