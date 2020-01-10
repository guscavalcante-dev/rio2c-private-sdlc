// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceSynopsisDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceSynopsisDto</summary>
    public class ConferenceSynopsisDto
    {
        public ConferenceSynopsis ConferenceSynopsis { get; set; }
        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceSynopsisDto"/> class.</summary>
        public ConferenceSynopsisDto()
        {
        }
    }
}