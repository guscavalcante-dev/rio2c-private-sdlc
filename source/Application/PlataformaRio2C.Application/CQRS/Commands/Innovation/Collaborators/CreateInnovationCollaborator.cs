// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-12-2019
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
        public CreateInnovationCollaborator(List<InnovationOption> innovationOptions)
        {
            this.UpdateBaseModelsAndLists(innovationOptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationCollaborator" /> class.
        /// </summary>
        public CreateInnovationCollaborator()
        {
        }
    }
}