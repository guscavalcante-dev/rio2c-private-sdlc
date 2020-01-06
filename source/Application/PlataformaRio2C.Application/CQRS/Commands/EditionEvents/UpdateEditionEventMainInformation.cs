// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="UpdateEditionEventMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateEditionEventMainInformation</summary>
    public class UpdateEditionEventMainInformation : CreateEditionEvent
    {
        public Guid EditionEventUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionEventMainInformation"/> class.</summary>
        /// <param name="editionEventDto">The edition event dto.</param>
        public UpdateEditionEventMainInformation(EditionEventDto editionEventDto)
            : base(editionEventDto)
        {
            this.EditionEventUid = editionEventDto?.EditionEvent?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionEventMainInformation"/> class.</summary>
        public UpdateEditionEventMainInformation()
        {
        }
    }
}