// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="RefuseMusicProjectEvaluation.cs" company="Softo">
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
    /// <summary>RefuseMusicProjectEvaluation</summary>
    public class RefuseMusicProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

        [Display(Name = "Reason", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectEvaluationRefuseReasonUid { get; set; }

        public bool HasAdditionalInfo { get; set; }

        [Display(Name = "Reason", ResourceType = typeof(Labels))]
        //[RequiredIf("HasAdditionalInfo", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(500, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Reason { get; set; }

        public MusicProjectDto MusicProjectDto { get; private set; }
        public List<ProjectEvaluationRefuseReason> ProjectEvaluationRefuseReasons { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RefuseMusicProjectEvaluation"/> class.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public RefuseMusicProjectEvaluation(
            MusicProjectDto musicProjectDto, 
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.UpdateModelsAndLists(musicProjectDto, projectEvaluationRefuseReasons);
            this.HasAdditionalInfo = false;
        }

        /// <summary>Initializes a new instance of the <see cref="RefuseMusicProjectEvaluation"/> class.</summary>
        public RefuseMusicProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public void UpdateModelsAndLists(
            MusicProjectDto musicProjectDto, 
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.ProjectUid = musicProjectDto?.MusicProject?.Uid;
            this.MusicProjectDto = musicProjectDto;
            this.ProjectEvaluationRefuseReasons = projectEvaluationRefuseReasons;
        }
    }
}