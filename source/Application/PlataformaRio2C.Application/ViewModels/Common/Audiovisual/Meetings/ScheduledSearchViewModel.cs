// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan valentim
// Created          : 09-27-2021
//
// Last Modified By : Renan valentim
// Last Modified On : 09-27-2021
// ***********************************************************************
// <copyright file="ScheduledSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ScheduledSearchViewModel</summary>
    public class ScheduledSearchViewModel
    {
        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public Guid? BuyerOrganizationUid { get; set; }

        [Display(Name = "Producer", ResourceType = typeof(Labels))]
        public Guid? SellerOrganizationUid { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Labels))]
        public DateTime? Date { get; set; }

        [Display(Name = "Project", ResourceType = typeof(Labels))]
        public string ProjectKeywords { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ScheduledSearchViewModel"/> class.</summary>
        public ScheduledSearchViewModel()
        {
        }
    }
}