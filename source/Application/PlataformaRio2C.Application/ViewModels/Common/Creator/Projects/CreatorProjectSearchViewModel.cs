// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="CreatorProjectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CreatorProjectSearchViewModel</summary>
    public class CreatorProjectSearchViewModel
    {
        [Display(Name = nameof(Labels.Search), ResourceType = typeof(Labels))]
        public string SearchKeywords { get; set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public Guid? EvaluationStatusUid { get; set; }

        [Display(Name = nameof(Labels.ShowPitchings), ResourceType = typeof(Labels))]
        public bool ShowBusinessRounds { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Id { get; set; }


        public List<ProjectEvaluationStatus> ProjectEvaluationStatuses;

        public X.PagedList.IPagedList<CreatorProject> AttendeeCreatorOrganizationDtos;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatorProjectSearchViewModel"/> class.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="id">The identifier.</param>
        public CreatorProjectSearchViewModel(
            string search,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int? page = 1,
            int? pageSize = 10,
            int? id = null)
        {
            this.SearchKeywords = search;
            this.EvaluationStatusUid = evaluationStatusUid;
            this.ShowBusinessRounds = showBusinessRounds;
            this.Page = page;
            this.PageSize = pageSize;
            this.Id = id;
        }

        /// <summary>Initializes a new instance of the <see cref="CreatorProjectSearchViewModel"/> class.</summary>
        public CreatorProjectSearchViewModel()
        {
            if (!this.Page.HasValue)
            {
                this.Page = 1;
            }

            if (!this.PageSize.HasValue)
            {
                this.PageSize = 12;
            }
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            string userInterfaceLanguage)
        {
            this.ProjectEvaluationStatuses = projectEvaluationStatuses.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
        }
    }
}