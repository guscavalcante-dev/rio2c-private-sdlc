// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 20-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 20-03-2021
// ***********************************************************************
// <copyright file="UpdateEditionDatesInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateEditionDatesInformation</summary>
    public class UpdateEditionDatesInformation : CreateEdition
    {
        public Guid EditionUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionDatesInformation"/> class.</summary>
        /// <param name="editionDto">The edition event dto.</param>
        public UpdateEditionDatesInformation(EditionDto editionDto) : base(editionDto)
        {
            this.EditionUid = editionDto?.Edition?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionDatesInformation"/> class.</summary>
        public UpdateEditionDatesInformation()
        {
        }
    }
}