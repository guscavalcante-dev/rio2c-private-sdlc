// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="EditionEventDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>EditionEventDto</summary>
    public class EditionEventDto
    {
        public EditionEvent EditionEvent { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EditionEventDto"/> class.</summary>
        public EditionEventDto()
        {
        }
    }
}