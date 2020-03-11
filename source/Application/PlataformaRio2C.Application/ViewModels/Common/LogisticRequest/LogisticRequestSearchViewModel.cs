// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="LogisticSponsorSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>LogisticSponsorSearchViewModel</summary>
    public class LogisticRequestSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }
        
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))] 
        public bool ShowAllParticipants { get; set; }

        [Display(Name = "ShowAllSponsors", ResourceType = typeof(Labels))] 
        public bool ShowAllSponsors { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorSearchViewModel"/> class.</summary>
        public LogisticRequestSearchViewModel()
        {
        }
    }
}