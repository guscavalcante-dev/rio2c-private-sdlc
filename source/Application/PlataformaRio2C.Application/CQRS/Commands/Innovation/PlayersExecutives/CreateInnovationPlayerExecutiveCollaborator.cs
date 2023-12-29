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
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateInnovationPlayerExecutiveCollaborator : InnovationPlayerExecutiveCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationPlayerExecutiveCollaborator" /> class.
        /// </summary>
        /// <param name="activities">The activities.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public CreateInnovationPlayerExecutiveCollaborator(
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            base.UpdateBaseProperties(
                null,
                activities,
                interestsDtos,
                innovationOrganizationTrackOptionDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationPlayerExecutiveCollaborator" /> class.
        /// </summary>
        public CreateInnovationPlayerExecutiveCollaborator()
        {
        }
    }
}