// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-11-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2023
// ***********************************************************************
// <copyright file="InnovationProjectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>InnovationProjectSearchViewModel</summary>
    public class InnovationProjectSearchViewModel
    {
        [Display(Name = nameof(Labels.Search), ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = nameof(Labels.Vertical), ResourceType = typeof(Labels))]
        public Guid? InnovationOrganizationTrackOptionGroupUid { get; set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public Guid? EvaluationStatusUid { get; set; }

        [Display(Name = nameof(Labels.ShowPitchings), ResourceType = typeof(Labels))]
        public bool ShowBusinessRounds { get; set; }
        
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Id { get; set; }

        public List<InnovationOrganizationTrackOptionGroup> InnovationOrganizationTrackOptionGroups;

        public List<ProjectEvaluationStatus> ProjectEvaluationStatuses;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationProjectSearchViewModel"/> class.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUid">The innovation organization track option group uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="id">The identifier.</param>
        public InnovationProjectSearchViewModel(
            string search,
            Guid? innovationOrganizationTrackOptionGroupUid,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int? page = 1,
            int? pageSize = 10,
            int? id = null)
        {
            this.Search = search;
            this.InnovationOrganizationTrackOptionGroupUid = innovationOrganizationTrackOptionGroupUid;
            this.EvaluationStatusUid = evaluationStatusUid;
            this.ShowBusinessRounds = showBusinessRounds;
            this.Page = page;
            this.PageSize = pageSize;
            this.Id = id;
        }

        /// <summary>Initializes a new instance of the <see cref="InnovationProjectSearchViewModel"/> class.</summary>
        public InnovationProjectSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroups">The innovation organization track option groups.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<InnovationOrganizationTrackOptionGroup> innovationOrganizationTrackOptionGroups, 
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            string userInterfaceLanguage)
        {
            this.InnovationOrganizationTrackOptionGroups = innovationOrganizationTrackOptionGroups.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
            this.ProjectEvaluationStatuses = projectEvaluationStatuses.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
        }
    }
}