// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-05-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-05-2024
// ***********************************************************************
// <copyright file="EvaluateCreatorProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EvaluateCreatorProject</summary>
    public class EvaluateCreatorProject : BaseCommand
    {
        [Display(Name = "CreatorProject", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CreatorProjectId { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public decimal? Grade { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluateCreatorProject" /> class.
        /// </summary>
        /// <param name="creatorProjectId">The creator project identifier.</param>
        /// <param name="grade">The grade.</param>
        public EvaluateCreatorProject(int creatorProjectId, decimal? grade)
        {
            this.CreatorProjectId = creatorProjectId;
            this.Grade = grade;
        }
    }
}