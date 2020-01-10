// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferencePresentationFormatDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferencePresentationFormatDto</summary>
    public class ConferencePresentationFormatDto
    {
        public ConferencePresentationFormat ConferencePresentationFormat { get; set; }
        public PresentationFormat PresentationFormat { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferencePresentationFormatDto"/> class.</summary>
        public ConferencePresentationFormatDto()
        {
        }
    }
}