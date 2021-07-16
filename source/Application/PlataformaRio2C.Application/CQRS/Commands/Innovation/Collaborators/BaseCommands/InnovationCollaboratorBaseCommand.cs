// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-08-2021
// ***********************************************************************
// <copyright file="InnovationCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>InnovationCollaboratorBaseCommand</summary>
    public class InnovationCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        [Display(Name = nameof(Labels.Tracks), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<Guid?> InnovationOrganizationTrackOptionsUids { get; set; }

        public List<InnovationOrganizationTrackOption> InnovationOrganizationTrackOptions { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationCollaboratorBaseCommand"/> class.</summary>
        public InnovationCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        public void UpdateBaseModelsAndLists(List<InnovationOrganizationTrackOption> innovationOrganizationTrackOptions)
        {
            this.InnovationOrganizationTrackOptions = innovationOrganizationTrackOptions;
        }
    }
}