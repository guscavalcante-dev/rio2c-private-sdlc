// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-29-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="CreateInnovationPlayerExecutiveCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateInnovationPlayerExecutiveCollaborator : InnovationCommissionCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationPlayerExecutiveCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public CreateInnovationPlayerExecutiveCollaborator(List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.UpdateBaseProperties(null, innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationPlayerExecutiveCollaborator" /> class.
        /// </summary>
        public CreateInnovationPlayerExecutiveCollaborator()
        {
        }
    }
}