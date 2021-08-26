// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-23-2021
// ***********************************************************************
// <copyright file="AudiovisualComissionEvaluateProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AudiovisualComissionEvaluateProject</summary>
    public class AudiovisualComissionEvaluateProject : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? ProjectId { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public decimal? Grade { get; set; }

        public Project Project { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualComissionEvaluateProject"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="grade">The grade.</param>
        public AudiovisualComissionEvaluateProject(Project project, decimal? grade)
        {
            this.Grade = grade;
            this.UpdateModelsAndLists(project);
        }

        /// <summary>Initializes a new instance of the <see cref="AudiovisualComissionEvaluateProject"/> class.</summary>
        public AudiovisualComissionEvaluateProject()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="project">The music project dto.</param>
        public void UpdateModelsAndLists(Project project)
        {
            this.ProjectId = project.Id;
            this.Project = project;
        }
    }
}