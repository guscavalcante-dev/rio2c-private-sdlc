// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2021
// ***********************************************************************
// <copyright file="CreateEdition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateEdition</summary>
    public class CreateEdition : BaseCommand
    {
        public EditionMainInformationBaseCommand EditionMainInformation { get; set; }
        public EditionDateBaseCommand EditionDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdition"/> class.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        public CreateEdition(EditionDto editionDto)
        {
            this.EditionMainInformation = new EditionMainInformationBaseCommand(editionDto);
            this.EditionDate = new EditionDateBaseCommand(editionDto);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdition" /> class.
        /// </summary>
        public CreateEdition()
        {
        }
    }
}