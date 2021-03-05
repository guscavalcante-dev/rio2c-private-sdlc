// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-03-2021
// ***********************************************************************
// <copyright file="UpdateEditionMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateEditionMainInformation</summary>
    public class UpdateEditionMainInformation : CreateEdition
    {
        public Guid EditionUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionMainInformation"/> class.</summary>
        /// <param name="editionDto">The edition event dto.</param>
        public UpdateEditionMainInformation(EditionDto editionDto)
            : base(editionDto)
        {
            this.EditionUid = editionDto?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionMainInformation"/> class.</summary>
        public UpdateEditionMainInformation()
        {
        }
    }
}