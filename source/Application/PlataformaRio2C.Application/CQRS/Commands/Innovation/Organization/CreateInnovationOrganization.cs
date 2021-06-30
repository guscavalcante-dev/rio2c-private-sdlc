// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 28-06-2021
// ***********************************************************************
// <copyright file="CreateInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationOrganization</summary>
    public class CreateInnovationOrganization : BaseCommand
    {
        public InnovationOrganizationApiDto InnovationOrganizationApiDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganization"/> class.
        /// </summary>
        /// <param name="innovationOrganizationApiDto">The innovation organization API dto.</param>
        public CreateInnovationOrganization(InnovationOrganizationApiDto innovationOrganizationApiDto)
        {
            this.InnovationOrganizationApiDto = innovationOrganizationApiDto;
        }
    }
}