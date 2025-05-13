// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="EvaluateInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EvaluateInnovationOrganization</summary>
    public class EvaluateInnovationOrganization : BaseCommand
    {
        [Display(Name = "InnovationOrganization", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? InnovationOrganizationId { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public decimal? Grade { get; set; }

        public InnovationOrganization InnovationOrganization { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EvaluateInnovationOrganization"/> class.</summary>
        /// <param name="innovationOrganization">The music project dto.</param>
        public EvaluateInnovationOrganization(InnovationOrganization innovationOrganization, decimal? grade)
        {
            this.Grade = grade;
            this.UpdateModelsAndLists(innovationOrganization);
        }

        /// <summary>Initializes a new instance of the <see cref="EvaluateInnovationOrganization"/> class.</summary>
        public EvaluateInnovationOrganization()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="innovationOrganization">The music project dto.</param>
        public void UpdateModelsAndLists(InnovationOrganization innovationOrganization)
        {
            this.InnovationOrganizationId = innovationOrganization.Id;
            this.InnovationOrganization = innovationOrganization;
        }
    }
}