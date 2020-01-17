// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 01/16/2020
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01/16/2020
// ***********************************************************************
// <copyright file="ReportsAudiovisualSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ReportsAudiovisualSearchViewModel</summary>
    public class ReportsAudiovisualSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        public string InterestUids { get; set; }

        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
        public string TargetAudienceUids { get; set; }

        [Display(Name = "ProjectsForPitching", ResourceType = typeof(Labels))]
        public bool IsPitching { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Labels))]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "EndDate", ResourceType = typeof(Labels))]
        public DateTime? EndDate { get; set; }


        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ReportsAudiovisualSearchViewModel"/> class.</summary>
        public ReportsAudiovisualSearchViewModel()
        {
        }
    }
}