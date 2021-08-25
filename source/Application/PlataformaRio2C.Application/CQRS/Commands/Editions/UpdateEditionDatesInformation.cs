// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-21-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2021
// ***********************************************************************
// <copyright file="UpdateEditionDatesInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateEditionDatesInformation</summary>
    public class UpdateEditionDatesInformation : EditionDateBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateEditionDatesInformation"/> class.</summary>
        /// <param name="editionDto">The edition event dto.</param>
        public UpdateEditionDatesInformation(EditionDto editionDto) 
            : base(editionDto)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEditionDatesInformation" /> class.
        /// </summary>
        public UpdateEditionDatesInformation()
        {
        }
    }
}