// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="CreateInnovationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationCollaborator</summary>
    public class CreateInnovationCollaborator : InnovationCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public CreateInnovationCollaborator(List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.UpdateBaseProperties(null, innovationOrganizationTrackOptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationCollaborator" /> class.
        /// </summary>
        public CreateInnovationCollaborator()
        {
        }
    }
}