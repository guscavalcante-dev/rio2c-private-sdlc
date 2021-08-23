// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-21-2021
// ***********************************************************************
// <copyright file="UpdateEditionMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateEditionMainInformation</summary>
    public class UpdateEditionMainInformation : EditionBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEditionMainInformation" /> class.
        /// </summary>
        /// <param name="editionDto">The edition event dto.</param>
        public UpdateEditionMainInformation(EditionDto editionDto) : base(editionDto, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEditionMainInformation"/> class.
        /// </summary>
        public UpdateEditionMainInformation()
        {
        }
    }
}