// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
// ***********************************************************************
// <copyright file="CreateInnovationCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateInnovationCommissionCollaborator : InnovationCommissionCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public CreateInnovationCommissionCollaborator(List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateBaseProperties(null, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationCommissionCollaborator" /> class.
        /// </summary>
        public CreateInnovationCommissionCollaborator()
        {
        }
    }
}