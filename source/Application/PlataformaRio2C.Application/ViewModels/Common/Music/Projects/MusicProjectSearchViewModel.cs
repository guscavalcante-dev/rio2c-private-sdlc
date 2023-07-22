// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-20-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-20-2023
// ***********************************************************************
// <copyright file="MusicProjectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>MusicProjectSearchViewModel</summary>
    public class MusicProjectSearchViewModel
    {
        [Display(Name = nameof(Labels.Search), ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = nameof(Labels.MusicGenre), ResourceType = typeof(Labels))]
        public Guid? MusicGenreUid { get; set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public Guid? EvaluationStatusUid { get; set; }

        [Display(Name = nameof(Labels.ShowBusinessRounds), ResourceType = typeof(Labels))]
        public bool ShowBusinessRounds { get; set; }

        [Display(Name = nameof(Labels.MusicGenre), ResourceType = typeof(Labels))]
        public IEnumerable<MusicGenre> MusicGenres { get; private set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public IEnumerable<ProjectEvaluationStatus> ProjectEvaluationStatuses { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectSearchViewModel"/> class.</summary>
        public MusicProjectSearchViewModel()
        {
            if (!this.Page.HasValue)
            {
                this.Page = 1;
            }

            if (!this.PageSize.HasValue)
            {
                this.PageSize = 10;
            }
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="musicGenres">The music genres.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<MusicGenre> musicGenres, 
            List<ProjectEvaluationStatus> projectEvaluationStatuses, 
            string userInterfaceLanguage)
        {
            this.MusicGenres = musicGenres.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
            this.ProjectEvaluationStatuses = projectEvaluationStatuses.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
        }
    }
}