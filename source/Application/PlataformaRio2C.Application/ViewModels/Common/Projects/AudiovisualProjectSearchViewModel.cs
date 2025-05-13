// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-20-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-30-2024
// ***********************************************************************
// <copyright file="AudiovisualProjectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>AudiovisualProjectSearchViewModel</summary>
    public class AudiovisualProjectSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "Genre", ResourceType = typeof(Labels))]
        public Guid? InterestUid { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Labels))]
        public Guid? EvaluationStatusUid { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public List<Interest> Interests;

        public List<ProjectEvaluationStatus> EvaluationStatuses;

        [Display(Name = "ProjectModality", ResourceType = typeof(Labels))]
        public Guid? ProjectModalityUid { get; set; }

        public List<ProjectModalityDto> ProjectModalities { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AudiovisualProjectSearchViewModel"/> class.</summary>
        public AudiovisualProjectSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="interests">The interests.</param>
        /// <param name="evaluationStatuses">The evaluation status.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="projectModalities">The project modality dto.</param>
        public void UpdateModelsAndLists(
            List<Interest> interests,
            List<ProjectEvaluationStatus> evaluationStatuses,
            string userInterfaceLanguage,
            List<ProjectModalityDto> projectModalities)
        {
            this.Interests = interests.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
            this.EvaluationStatuses = evaluationStatuses.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
            projectModalities.ForEach(g => g.Translate(userInterfaceLanguage));
            this.ProjectModalities = projectModalities;
        }
    }
}