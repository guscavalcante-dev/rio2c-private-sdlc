// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="EvaluateCartoonProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EvaluateCartoonProject</summary>
    public class EvaluateCartoonProject : BaseCommand
    {
        [Display(Name = "CartoonProject", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CartoonProjectId { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public decimal? Grade { get; set; }

        public CartoonProject CartoonProject { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EvaluateCartoonProject"/> class.</summary>
        /// <param name="cartoonProject">The music project dto.</param>
        public EvaluateCartoonProject(CartoonProject cartoonProject, decimal? grade)
        {
            this.Grade = grade;
            this.UpdateModelsAndLists(cartoonProject);
        }

        /// <summary>Initializes a new instance of the <see cref="EvaluateCartoonProject"/> class.</summary>
        public EvaluateCartoonProject()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="cartoonProject">The cartoon project dto.</param>
        public void UpdateModelsAndLists(CartoonProject cartoonProject)
        {
            this.CartoonProjectId = cartoonProject.Id;
            this.CartoonProject = cartoonProject;
        }
    }
}